namespace Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
}