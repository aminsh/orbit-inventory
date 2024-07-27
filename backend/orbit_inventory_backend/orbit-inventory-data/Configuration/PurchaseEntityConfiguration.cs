using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orbit_inventory_core.Data;
using orbit_inventory_domain;
using orbit_inventory_domain.entity;

namespace orbit_inventory_data.Configuration;

public class PurchaseEntityConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder
            .UseDefaultEntityConfig()
            .UseDefaultTimeStampsConfig()
            .UserDefaultCreatorConfiguration();

        builder.HasMany(p => p.Lines).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}