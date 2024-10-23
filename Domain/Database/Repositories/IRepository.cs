using WebApplication3.Domain.Database;

namespace WebApplication3.Domain.Data.Repositories
{
    internal interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}