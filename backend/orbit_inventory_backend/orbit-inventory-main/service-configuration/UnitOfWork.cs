using orbit_inventory_core.application;
using orbit_inventory_data;

namespace orbit_inventory_main.service_configuration;

public class UnitOfWork(OrbitDbContext orbitDbContext): IUnitOfWork
{
    public async Task Commit()
    {
        await orbitDbContext.SaveChangesAsync();
    }
}