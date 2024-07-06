using orbit_inventory_data;

namespace orbit_inventory_application;

public interface IUnitOfWork
{
    Task Commit();
}

public class UnitOfWork(OrbitDbContext orbitDbContext): IUnitOfWork
{
    
    public async Task Commit()
    {
        await orbitDbContext.SaveChangesAsync();
    }
}