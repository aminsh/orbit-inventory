using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using orbit_inventory_domain.Core;

namespace orbit_inventory_data;

public class EntityRepository<TEntity>(OrbitDbContext context) : IEntityRepository<TEntity>
    where TEntity : Entity
{
    public IQueryable<TEntity> Find()
    {
        return context.Set<TEntity>();
    }

    public Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate)
    {
        return Find().FirstOrDefaultAsync(predicate);
    }

    public Task<TEntity?> FindById(int id)
    {
        return FindOne(e => e.Id == id);
    }

    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }

    public void Attach(TEntity entity)
    {
        context.Attach(entity);
    }

    public void Remove(TEntity entity)
    {
        context.Remove(entity);
    }
}