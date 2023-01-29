namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IFeedRepository Feeds { get; }
        INewsRepository News { get; }

        Task SaveChangesAsync();
    }
}
