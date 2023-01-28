using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FeedRepository : IFeedRepository
    {
        private readonly RSSManagerDbContext _context;
        public FeedRepository(RSSManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Feed?> GetByIdAsync(int id)
        {
            return await _context.Feeds
                .Include(x => x.News)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Feed>> GetAllAsync()
        {
            return await _context.Feeds
                .Include(x => x.News)
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
            await _context.AddAsync(entity);
            return true;
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
