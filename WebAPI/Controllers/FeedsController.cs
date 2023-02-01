using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedsController : ControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedsController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FeedDto>))]
        public async Task<IActionResult> GetAll()
        {
            var feeds = await _feedService.GetAllAsync();

            return Ok(feeds);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FeedDto))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> AddFeed([FromQuery] string url)
        {
            var feed = await _feedService.AddFeedAsync(url);

            return CreatedAtAction(nameof(AddFeed), feed);
        }
    }
}
