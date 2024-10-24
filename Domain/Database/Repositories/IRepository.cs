using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Features.Friends.Entities;

namespace WebApplication3.Domain.Data.Repositories
{
     public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Add(TEntity entity);

        Task<TEntity?> GetByPropertyAsync<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);
        Task<TEntity?> GetByIdAsync(Guid id);

         Task<List<TEntity>> GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}