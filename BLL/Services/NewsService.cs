using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class NewsService: INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<IEnumerable<NewsDto>> GetUnreadNewsAsync(DateTime date)
        {
            var user = await _authService.GetCurrentUserAsync();
            var news = await _unitOfWork.News
                .FindAsync(x =>
                    x.UserNews
                        .Any(y => 
                            !y.IsRead && 
                            y.NewsId == x.Id &&
                            y.UserId == user?.Id) &&
                    x.PublishingDate >= date);

            var result = news.ToList();

            return _mapper.Map<IEnumerable<NewsDto>>(news);
        }

        public async Task<NewsDto> SetNewsAsReadAsync(string newsId)
        {
            var news = await _unitOfWork.News.GetByIdAsync(newsId);
            if (news == null)
                throw new NotFoundException("News with specified id was not found");

            news.UserNews
                .First(x => x.NewsId== newsId)
                .IsRead = true;

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<NewsDto>(news);
        }
    }
}
