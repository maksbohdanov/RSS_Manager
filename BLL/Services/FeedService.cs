using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using CodeHollow.FeedReader;
using DAL.Interfaces;
using BLL.Exceptions;
using Feed = DAL.Entities.Feed;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;

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
            var user = await _authService.GetCurrentUserAsync();
            var feeds = await _unitOfWork.Feeds
                .FindAsync(x => x.Users.Any(x => x.Id == user?.Id));

            return _mapper.Map<IEnumerable<FeedDto>>(feeds, opt => opt.Items["UserId"] = user?.Id);
        }        

        public async Task<FeedDto> AddFeedAsync(string url)
        {
            var feedFromUrl = await ReadFeedAsync(url);
            var user = await _authService.GetCurrentUserAsync();

            var newFeed = _mapper.Map<CodeHollow.FeedReader.Feed, Feed>(feedFromUrl);
            if(user != null)
            {
                newFeed.Users.Add(user);
            }

            var entityCreated = await _unitOfWork.Feeds.CreateAsync(newFeed);

            var result = entityCreated ? newFeed : await FindFeedAsync(newFeed);

            if(user != null && result != null)
            {
                AddUserToNews(user, result.News);
            }
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FeedDto>(result, opt => opt.Items["UserId"] = user?.Id);
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

        private async Task<Feed?> FindFeedAsync(Feed feed)
        {
            var feedFromDb = (await _unitOfWork.Feeds
                .FindAsync(x => x.Link == feed.Link && x.Title == feed.Title))
                .FirstOrDefault();

            if(feedFromDb != null)
                await UpdateExistingFeedAsync(feedFromDb, feed.Users.Last());

            return feedFromDb;
        }

        private async Task UpdateExistingFeedAsync(Feed feed, IdentityUser user)
        {
            if(feed.Users.Contains(user))
                throw new RssManagerException($"{nameof(Feed)} already exists.");

            feed.Users.Add(user);
            await _unitOfWork.Feeds.UpdateAsync(feed);           
        }

        private void AddUserToNews(IdentityUser user, IEnumerable<News> news)
        {
            foreach (var newsItem in news)
            {
                newsItem.Users.Add(user);
            }
        }
    }
}
