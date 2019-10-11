using Binjy.HackerNews.Core.Interface;
using Binjy.HackerNews.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerController : ControllerBase
    {
        private readonly IHackerNewsClient<Story> storyClient;

        private readonly IHackerNewsClient<Comment> commentClient;

        private ILogger logger;

        public HackerController(IHackerNewsClient<Story> storyClient,
            IHackerNewsClient<Comment> commentClient, ILogger<HackerController> logger)
        {
            this.storyClient = storyClient;
            this.commentClient = commentClient;
            this.logger = logger;
        }

        // GET api/hacker/stories/newest/{limit}
        [HttpGet("stories/newest/{limit?}")]
        public async Task<List<Story>> NewestStories(int limit = 10)
        {
            List<int> storyIndex = await storyClient.IndexItems("newstories", limit);
            List<Story> stories = await storyClient.GetItemsForIndex(storyIndex.Take(limit).ToList());

            return stories;
        }

        // GET api/hacker/stories/top/{limit}{
        [HttpGet("stories/top/{limit?}")]
        public async Task<List<Story>> TopStories(int limit = 10)
        {
            List<int> storyIndex = await storyClient.IndexItems("topstories", limit);
            List<Story> stories = await storyClient.GetItemsForIndex(storyIndex.Take(limit).ToList());

            return stories;
        }

        // GET api/hacker/stories/{storyId}
        [HttpGet("stories/{storyId}")]
        public async Task<Story> GetStoryById(int storyId)
        {
            return await storyClient.GetItemById(storyId);
        }

        // GET api/hacker/stories/{storyId}/comments
        [HttpGet("stories/{storyId}/comments")]
        public async Task<List<Comment>> StoryComments(int storyId)
        {
            Story story = await storyClient.GetItemById(storyId);

            List<Comment> storyComments = await commentClient.GetItemsForIndex(story.CommentIndex);

            return storyComments;
        }
    }
}
