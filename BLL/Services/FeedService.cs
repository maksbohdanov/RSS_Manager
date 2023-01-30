using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using CodeHollow.FeedReader;
using DAL.Interfaces;
using BLL.Exceptions;
using Feed = DAL.Entities.Feed;

namespace BLL.Services
{
    public class FeedService : IFeedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public FeedService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<IEnumerable<FeedDto>> GetAllAsync()
        {
            var userId = _authService.GetCurrentUserId();
            var feeds = await _unitOfWork.Feeds
                .FindAsync(x => x.UserId == userId);
            
            return _mapper.Map<IEnumerable<FeedDto>>(feeds);
        }

        public async Task<FeedDto> AddFeedAsync(string url)
        {
            var feedFromUrl = await ReadFeedAsync(url);
            var userId = _authService.GetCurrentUserId();

            var newFeed = _mapper.Map<CodeHollow.FeedReader.Feed, Feed>(feedFromUrl, opt => opt.Items["UserId"] = userId);

            await _unitOfWork.Feeds.CreateAsync(newFeed);
            await _unitOfWork.SaveChangesAsync();

            var createdFeed = await _unitOfWork.Feeds.GetByIdAsync(newFeed.Id);
            return _mapper.Map<FeedDto>(createdFeed);

        }

        public async Task<CodeHollow.FeedReader.Feed> ReadFeedAsync(string url)
        {            
            try
            {
                var feed = await FeedReader.ReadAsync(url);
                return feed;
            }
            catch (Exception ex)
            {
                throw new RssManagerException("Wrong URL. An error occurred while trying to read the feed from a given URL.", ex);
            }
        }
    }
}
