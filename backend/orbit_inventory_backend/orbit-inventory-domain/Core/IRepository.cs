using System.Linq.Expressions;

namespace orbit_inventory_domain.Core;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> Find();

    Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FindById(int id);

    void Add(TEntity entity);

    void Attach(TEntity entity);

    void Remove(TEntity entity);
}