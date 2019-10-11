using Binjy.HackerNews.Core.Interface;
using System;

namespace Binjy.HackerNews.Core.Service
{
    /// <summary>
    /// API Configuration object for use with HackerNews API
    /// </summary>
    public class RestApiConfig : IApiConfig
    {
        // Base URI of the API
        public string BaseUri { get; set; } = "hacker-news.firebaseio.com";

        // Version identifier for the API
        public string ApiVersion { get; set; } = "v0";

        // Indicates if config should prepare URLs against SSL
        public bool IsSecure { get; set; } = true;

        // Resolve the correct HTTP/s protocol
        private string Protocol
        {
            get
            {
                if (IsSecure)
                {
                    return "https";
                }
                else
                {
                    return "http";
                }
            }
        }

        /// <summary>
        /// Generates URI to API endpoint retrieve list of Items.
        /// </summary>
        /// <returns>URI of API index endpoint.</returns>
        public Uri AsIndexEndpointUrl(string endpoint)
        {
            checkProperties();

            return new Uri($"{Protocol}://{BaseUri}/{ApiVersion}/{endpoint}.json");
        }

        /// <summary>
        /// Generates URI to API endpoint to get specific item by Id 
        /// </summary>
        /// <param name="id">Id of item to retrieve.</param>
        /// <returns></returns>
        public Uri AsItemEndpointUrl(int id)
        {
            checkProperties();

            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return new Uri($"{Protocol}://{BaseUri}/{ApiVersion}/item/{id}.json");
        }

        /// <summary>
        /// Validate good state of object
        /// </summary>
        private void checkProperties()
        {
            if (String.IsNullOrEmpty(BaseUri))
            {
                throw new ArgumentNullException(nameof(BaseUri));
            }

            if (String.IsNullOrEmpty(ApiVersion))
            {
                throw new ArgumentNullException(nameof(ApiVersion));
            }


        }
    }
}