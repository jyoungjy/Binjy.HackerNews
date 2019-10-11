using System.Runtime.Serialization;

namespace Binjy.HackerNews.Core.Model
{
    /// <summary>
    /// Story class representing story objects obtained from https://github.com/HackerNews/API
    /// </summary>
    [DataContract]
    public class Story : Item
    {
        [DataMember(Name = "title", IsRequired = true)]
        public string Title { get; set; }

        [DataMember(Name = "score", IsRequired = false)]
        public int Score { get; set; }

        [DataMember(Name = "url", IsRequired = false)]
        public string Url { get; set; }

        // per documentation, in case of story the descendents property
        // represents the count of comments so projecting that fact here
        // in this api version
        [DataMember(Name = "descendants", IsRequired = false)]
        public int CommentCount { get; set; }
    }
}
