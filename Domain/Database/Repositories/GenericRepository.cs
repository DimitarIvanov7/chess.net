using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Friends.Entities;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Data.Repositories;

public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly ApplicationDbContext DbContext;

    protected GenericRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public abstract Task<List<TEntity>> GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);
    

    public async virtual Task<TEntity?> GetByPropertyAsync<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
    {
        return await DbContext.Set<TEntity>()
                              .FirstOrDefaultAsync(entity => EF.Property<TProperty>(entity, ((MemberExpression)propertyExpression.Body).Member.Name)!.Equals(value));
    }
    public async virtual Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}