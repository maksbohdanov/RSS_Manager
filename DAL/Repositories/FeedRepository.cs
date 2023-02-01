using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FeedRepository : IFeedRepository
    {
        private readonly RssManagerDbContext _context;
        public FeedRepository(RssManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Feed?> GetByIdAsync(int id)
        {
            return await _context.Feeds
                .Include(x => x.News)
                .ThenInclude(x => x.UserNews)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Feed>> GetAllAsync()
        {
            return await _context.Feeds
                .Include(x => x.News)
                .ThenInclude(x => x.UserNews)
                .Include(x => x.Users)
                .ToListAsync();
        }

        public async Task<IEnumerable<Feed>> FindAsync(Func<Feed, bool> predicate)
        {
            return (await GetAllAsync())
                .Where(predicate);
        }

        public async Task<bool> UpdateAsync(Feed entity)
        {
            var dbEntity = await GetByIdAsync(entity.Id);
            if (dbEntity == null)
            {
                return false;
            }
            _context.Update(entity);
            return true;
        }

        public async Task<bool> CreateAsync(Feed entity)
        {
            var entityExists = await CheckIfExists(entity);
            if (entityExists)
                return false;

            await _context.AddAsync(entity);
            return true;
        }

        public async Task<bool> CheckIfExists(Feed entity)
        {
            return await _context.Feeds
                .AnyAsync(x => x.Link == entity.Link && x.Title == entity.Title);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var dbEntity = await GetByIdAsync(id);
            if (dbEntity == null)
            {
                return false;
            }

            _context.Remove(dbEntity);
            return true;
        }
    }
}
