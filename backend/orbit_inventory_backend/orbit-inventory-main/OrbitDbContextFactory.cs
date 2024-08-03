using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using orbit_inventory_data;
using orbit_inventory_main.Authentication;

namespace orbit_inventory_main;

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
                b => b.MigrationsAssembly("orbit-inventory-main")
            )
            .UseSnakeCaseNamingConvention();

        return new OrbitDbContext(optionsBuilder.Options, new OrbitHttpRequestContext());
    }
}