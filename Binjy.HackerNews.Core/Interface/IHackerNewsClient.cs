using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binjy.HackerNews.Core.Interface
{
    /// <summary>
    /// Interface representing core behaviors needed to call https://github.com/HackerNews/API
    /// </summary>
    /// <typeparam name="T">type of item to create client for</typeparam>
    public interface IHackerNewsClient<T> where T : IHackerItem
    {
        Task<List<int>> IndexItems(string endpoint, int limit);

        Task<List<T>> GetItemsForIndex(List<int> index);

        Task<T> GetItemById(int id);
    }
}
