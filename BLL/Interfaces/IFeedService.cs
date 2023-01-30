using BLL.Models;
using CodeHollow.FeedReader;

namespace BLL.Interfaces
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedDto>> GetAllAsync();
        Task<FeedDto> AddFeedAsync(string url);
        Task<Feed> ReadFeedAsync(string url);
    }
}
