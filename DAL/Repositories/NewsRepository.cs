using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class NewsRepository: INewsRepository
    {
        private readonly RSSManagerDbContext _context;
        public NewsRepository(RSSManagerDbContext context)
        {
            _context = context;
        }

        public async Task<News?> GetByIdAsync(string id)
        {
            return await _context.News
                .Include(x => x.Feed)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News
                .Include(x => x.Feed)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> FindAsync(Func<News, bool> predicate)
        {
            return (await GetAllAsync())
                .Where(predicate);
        }

        public async Task<bool> UpdateAsync(News entity)
        {
            var dbEntity = await GetByIdAsync(entity.Id);
            if (dbEntity == null)
            {
                return false;
            }
            _context.Update(entity);
            return true;
        }

        public async Task<bool> CreateAsync(News entity)
        {
            await _context.AddAsync(entity);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(string id)
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
