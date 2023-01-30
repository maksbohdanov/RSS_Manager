using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RssManagerDbContext _context;

        public IFeedRepository Feeds { get; }

        public INewsRepository News { get; }

        public UnitOfWork(RssManagerDbContext context, IFeedRepository feedRepository, INewsRepository newsRepository)
        {
            _context = context;
            Feeds = feedRepository;
            News = newsRepository;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
