using Microsoft.EntityFrameworkCore;
using orbit_inventory_core.Domain;
using orbit_inventory_core.request;

namespace orbit_inventory_data;

public class OrbitDbContext(
    DbContextOptions<OrbitDbContext> options,
    IOrbitRequestContext orbitRequestContext
) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrbitDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        var currentUser = orbitRequestContext.UserId == 0
            ? null
            : await Set<User>().FindAsync([orbitRequestContext.UserId], cancellationToken: cancellationToken);

        var addedEntries = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added).ToList();

        foreach (var entity in addedEntries.Select(entry => entry.Entity))
        {
            if (entity is IHaveCreator creator)
                if (currentUser != null)
                    creator.CreatedBy = currentUser;

            if (entity is IHaveTimestamps timestamps)
            {
                timestamps.CreatedAt = DateTime.UtcNow;
                timestamps.UpdatedAt = DateTime.UtcNow;
            }
        }

        var modifiedEntries = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Modified).ToList();

        foreach (var entity in modifiedEntries.Select(entry => entry.Entity))
        {
            if (entity is IHaveTimestamps timestamps)
                timestamps.UpdatedAt = DateTime.UtcNow;
        }
        
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}