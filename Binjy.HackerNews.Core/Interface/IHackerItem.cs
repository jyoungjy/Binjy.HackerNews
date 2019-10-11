namespace Binjy.HackerNews.Core.Interface
{
    public enum ItemType
    {
        Unknown,
        Job,
        Story,
        Comment,
        Poll,
        Pollopt
    }

    public interface IHackerItem
    {
        int Id { get; set; }
    }
}