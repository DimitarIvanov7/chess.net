using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Data.Repositories;

internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async virtual Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public abstract Task<List<TEntity>> GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);


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