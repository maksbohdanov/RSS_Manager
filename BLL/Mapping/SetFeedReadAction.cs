using AutoMapper;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BLL.Mapping
{
    public class SetFeedReadAction : IMappingAction<News, NewsDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SetFeedReadAction(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor= httpContextAccessor;
        }

        public void Process(News source, NewsDto destination, ResolutionContext context)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isRead = source.UserNews
                .First(x => x.NewsId == source.Id && x.UserId == userId)
                .IsRead;

            destination.IsRead = isRead;
        }
    }
}
