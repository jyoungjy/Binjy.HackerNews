using Binjy.HackerNews.Core.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Binjy.HackerNews.Core.Service
{
    /// <summary>
    /// HackerNewsRestClient to call HackerNews API documented at https://github.com/HackerNews/API
    /// </summary>
    /// <typeparam name="T">type of item to create client for</typeparam>
    public class HackerNewsRestClient<T>
        : IHackerNewsClient<T> where T : IHackerItem
    {
        private readonly IApiConfig apiConfig;

        private readonly ILogger<IHackerNewsClient<T>> logger;

        private readonly IItemMapper<T> itemMapper;

        private readonly IHttpClientFactory clientFactory;

        private HashSet<int> indexCache = new HashSet<int>();

        public bool EnableCache { get; set; } = true;

        /// <summary>
        /// Constructor for BaseHackerRestClient
        /// </summary>
        /// <param name="config">RestApiConfig containing details about Rest API to call</param>
        /// <param name="logger">ILogger implementation for logging</param>
        /// <param name="itemMapper">IItemMapper implementation for parsing the type from the response.</param>
        public HackerNewsRestClient(IApiConfig config,
            IHttpClientFactory clientFactory,
            ILogger<IHackerNewsClient<T>> logger,
            IItemMapper<T> itemMapper)
        {
            this.apiConfig = config;
            this.clientFactory = clientFactory;
            this.logger = logger;
            this.itemMapper = itemMapper;
        }

        #region IHackerNewsClient Implementation
        /// <summary>
        /// Given an API endpoint that provides list of items.
        /// </summary>
        /// <returns></returns>
        public async Task<List<int>> IndexItems(string endpoint, int limit)
        {
            // acquire client from pool using IHttpClientFactory
            using (HttpClient client = clientFactory.CreateClient())
            {
                // logging uri 
                logger.LogInformation(apiConfig.AsIndexEndpointUrl(endpoint).AbsoluteUri);

                // getting index array
                var indexResponse = await client.GetAsync(apiConfig.AsIndexEndpointUrl(endpoint));
                indexResponse.EnsureSuccessStatusCode();

                // reading result and deserializing index 
                var indexResult = await indexResponse.Content.ReadAsStringAsync();
                var indexValues = JsonConvert.DeserializeObject<List<int>>(indexResult);

                //adding items to cache
                if (EnableCache)
                {
                    foreach (int i in indexValues.GetRange(0, limit))
                    {
                        indexCache.Add(i);
                    }
                }

                return indexValues;//.GetRange(0, 10);
            }

        }

        /// <summary>
        /// Get a specific item by Id
        /// </summary>
        /// <param name="id">Id of the item to return</param>
        /// <returns>Instance of IHackerItem with matching the referenced id.</returns>
        public async Task<T> GetItemById(int id)
        {
            using (HttpClient client = clientFactory.CreateClient())
            {
                // getting item for id from api
                var itemResponse = await client.GetAsync(apiConfig.AsItemEndpointUrl(id));
                itemResponse.EnsureSuccessStatusCode();

                // read response
                var itemResult = await itemResponse.Content.ReadAsStringAsync();

                // use the specific mapper to map the item
                var mappedItem = itemMapper.MapItem(itemResult);

                if (mappedItem == null)
                {
                    logger.LogDebug(itemResult);
                }

                return mappedItem;
            }
        }

        /// <summary>
        /// Returns all items of T for given index of item ids.
        /// </summary>
        /// <param name="index">Enumerable of integers that represent the ids of each item to retrieve.</param>
        /// <returns></returns>
        public async Task<List<T>> GetItemsForIndex(List<int> index)
        {
            var items = new List<T>();
            var indexTasks = index.Select(entry => GetItemById(entry)).ToList();

            // Loop while no other tasks remain
            while (indexTasks.Count > 0)
            {
                // Get first task that completes
                Task<T> firstFinishedItem = await Task.WhenAny(indexTasks);

                // Remove task from list so that it isn't processed more than once
                indexTasks.Remove(firstFinishedItem);

                // Await the completed task
                T item = await firstFinishedItem;

                // Add item to list
                if (item != null)
                {
                    items.Add(item);
                }
                else
                {
                    logger.LogWarning("Dropped item!");
                }
            }

            return items;
        }
        #endregion
    }
}
