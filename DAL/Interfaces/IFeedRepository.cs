using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IFeedRepository: IRepository<Feed, int>
    {
        Task<bool> CheckIfExists(Feed entity);
    }
}
