using Microsoft.EntityFrameworkCore;

namespace orbit_inventory_data;

public class OrbitDbContext(DbContextOptions<OrbitDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrbitDbContext).Assembly);
    }
}