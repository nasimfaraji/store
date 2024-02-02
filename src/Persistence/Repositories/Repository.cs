using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly DbContext _dbContext;

    protected Repository(ApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public void Add(TEntity entity) =>
        _dbContext.Set<TEntity>().Add(entity);

    public void Add(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().AddRange(entities);

    public void Remove(TEntity entity) =>
        _dbContext.Set<TEntity>().Remove(entity);

    public void Remove(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().RemoveRange(entities);

    public void Update(TEntity entity) =>
        _dbContext.Set<TEntity>().Update(entity);

    public void Update(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().UpdateRange(entities);
}