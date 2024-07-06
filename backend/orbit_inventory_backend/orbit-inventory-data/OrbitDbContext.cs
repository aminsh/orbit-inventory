using Microsoft.EntityFrameworkCore;
using orbit_inventory_data.Configuration;

namespace orbit_inventory_data;

public class OrbitDbContext(DbContextOptions<OrbitDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}