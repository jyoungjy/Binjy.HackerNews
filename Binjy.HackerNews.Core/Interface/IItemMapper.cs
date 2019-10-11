namespace Binjy.HackerNews.Core.Interface
{
    /// <summary>
    /// Interface to define behavior of the mapping of IHackerItems 
    /// </summary>
    /// <typeparam name="T">type of IHackerItem</typeparam>
    public interface IItemMapper<T> where T : IHackerItem
    {
        T MapItem(string rawResult);
    }
}
