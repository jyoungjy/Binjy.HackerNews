using Binjy.HackerNews.Core.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Binjy.HackerNews.Core.Model
{
    /// <summary>
    /// Base class capturing most common elements observed within
    /// sample objects at https://github.com/HackerNews/API
    /// </summary>
    [DataContract()]
    public class Item : IHackerItem
    {
        // projecting the by parameter as the author
        [DataMember(Name = "by", IsRequired = false)]
        public string Author { get; set; }

        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        // per documentation the kids property represents the ids of comments
        // on the item
        [DataMember(Name = "kids", IsRequired = false)]
        public List<int> CommentIndex { get; set; }

        // default DateTime conversion expects .net compatible string
        // using special converter to handle unix timestamp format
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [DataMember(Name = "time", IsRequired = true)]
        public DateTime Time { get; set; }

        [DataMember(Name = "type", IsRequired = true)]
        public string Type { get; set; }
    }
}