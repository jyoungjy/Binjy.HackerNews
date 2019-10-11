using Binjy.HackerNews.Core.Interface;
using Binjy.HackerNews.Core.Model;
using Newtonsoft.Json;
using System;

namespace Binjy.HackerNews.Core.Utility
{
    public class StoryMapper : IItemMapper<Story>
    {
        private readonly JsonSerializerSettings serializerSettings;

        public StoryMapper(JsonSerializerSettings serializerSettings)
        {
            this.serializerSettings = serializerSettings;
        }

        public Story MapItem(string rawResult)
        {
            var rawObject = JsonConvert.DeserializeObject<Story>(rawResult, serializerSettings);
            ItemType parsedType = ItemType.Unknown;

            // quick exit if type doesn't parse
            if (rawObject == null)
                return null;

            if (!Enum.TryParse<ItemType>(rawObject.Type, true, out parsedType))
                return null;

            // only return objects of type Story
            if (parsedType == ItemType.Story)
            {
                var projectedStory = new Story
                {
                    Author = rawObject.Author,
                    Id = rawObject.Id,
                    Time = rawObject.Time,
                    Type = rawObject.Type,
                    CommentIndex = rawObject.CommentIndex,
                    Title = rawObject.Title,
                    Score = rawObject.Score,
                    Url = rawObject.Url,
                    CommentCount = rawObject.CommentCount

                };

                return projectedStory;
            }
            else
            {
                return null;
            }
        }
    }
}
