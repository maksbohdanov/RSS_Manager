namespace DAL.Interfaces
{
    public interface IRepository<TEntity, TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> DeleteByIdAsync(TKey id);
    }
}
