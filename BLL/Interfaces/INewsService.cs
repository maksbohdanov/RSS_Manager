using BLL.Models;

namespace BLL.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetUnreadNewsAsync(DateTime date);
        Task<NewsDto> SetNewsAsReadAsync(string newsId);
    }
}
