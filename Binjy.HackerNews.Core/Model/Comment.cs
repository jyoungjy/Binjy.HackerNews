using System.Runtime.Serialization;

namespace Binjy.HackerNews.Core.Model
{
    [DataContract]
    public class Comment : Item
    {
        [DataMember(Name = "text", IsRequired = false)]
        public string Text { get; set; }

        [DataMember(Name = "parent", IsRequired = false)]
        public int Parent { get; set; }
    }
}
