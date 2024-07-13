using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using orbit_inventory_core.Domain;

namespace orbit_inventory_data;

public class Repository<TEntity>(OrbitDbContext dbContext) : IRepository<TEntity> 
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    
    public IQueryable<TEntity> Query => _dbSet;

    public Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> prediction)
    {
        return _dbSet.FirstOrDefaultAsync(prediction);
    }

    public Task<TEntity?> FindById(int id)
    {
        return FindOne(p => p.Id == id);
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}