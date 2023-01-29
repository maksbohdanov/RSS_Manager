using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RSSManagerDbContext _context;

        public IFeedRepository Feeds { get; }

        public INewsRepository News { get; }

        public UnitOfWork(RSSManagerDbContext context, IFeedRepository feedRepository, INewsRepository newsRepository)
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
