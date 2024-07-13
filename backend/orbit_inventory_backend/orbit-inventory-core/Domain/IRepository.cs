using System.Linq.Expressions;

namespace orbit_inventory_core.Domain;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> Query { get; }

    Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> prediction);

    Task<TEntity?> FindById(int id);

    void Add(TEntity entity);

    void Remove(TEntity entity);
}