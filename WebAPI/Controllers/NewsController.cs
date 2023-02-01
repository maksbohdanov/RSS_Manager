using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NewsDto>))]
        public async Task<IActionResult> GetUnreadNews([FromQuery] DateTime date)
        {
            var news = await _newsService.GetUnreadNewsAsync(date);

            return Ok(news);
        }

        [HttpPut("read")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsDto))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> SetNewsAsRead([FromQuery] string newsId)
        {
            var news = await _newsService.SetNewsAsReadAsync(newsId);

            return Ok(news);
        }
    }
}
