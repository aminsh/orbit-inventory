using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using orbit_inventory_data;

namespace orbit_inventory_web;

public class OrbitDbContextFactory : IDesignTimeDbContextFactory<OrbitDbContext>
{
    public OrbitDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Postgres");

        var optionsBuilder = new DbContextOptionsBuilder<OrbitDbContext>();
        optionsBuilder.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly("orbit-inventory-web")
            )
            .UseSnakeCaseNamingConvention();

        return new OrbitDbContext(optionsBuilder.Options);
    }
}