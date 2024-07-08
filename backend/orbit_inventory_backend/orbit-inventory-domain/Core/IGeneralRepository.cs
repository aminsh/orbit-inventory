using System.Linq.Expressions;

namespace orbit_inventory_domain.Core;

public interface IGeneralRepository<TEntity, in TType>
{
    IQueryable<TEntity> Find();

    Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FindById(TType id);

    void Add(TEntity entity);

    void Attach(TEntity entity);

    void Remove(TEntity entity);
}