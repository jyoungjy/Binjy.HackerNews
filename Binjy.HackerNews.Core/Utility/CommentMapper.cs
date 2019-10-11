using Binjy.HackerNews.Core.Interface;
using Binjy.HackerNews.Core.Model;
using Newtonsoft.Json;
using System;

namespace Binjy.HackerNews.Core.Utility
{
    public class CommentMapper : IItemMapper<Comment>
    {
        private readonly JsonSerializerSettings serializerSettings;

        public CommentMapper(JsonSerializerSettings serializerSettings)
        {
            this.serializerSettings = serializerSettings;
        }

        public Comment MapItem(string rawResult)
        {
            var rawObject = JsonConvert.DeserializeObject<Comment>(rawResult, serializerSettings);
            ItemType parsedType = ItemType.Unknown;

            // quick exit if type doesn't parse
            if (!Enum.TryParse<ItemType>(rawObject.Type, true, out parsedType))
                return null;

            // only return objects of type Story
            if (parsedType == ItemType.Comment)
            {
                var projectedComment = new Comment
                {
                    Author = rawObject.Author,
                    Id = rawObject.Id,
                    Time = rawObject.Time,
                    Type = rawObject.Type,
                    CommentIndex = rawObject.CommentIndex,
                    Text = rawObject.Text,
                    Parent = rawObject.Parent
                };

                return projectedComment;
            }
            else
            {
                return null;
            }
        }
    }
}
