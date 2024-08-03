using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orbit_inventory_core.Data;
using orbit_inventory_domain;
using orbit_inventory_domain.entity;

namespace orbit_inventory_data.Configuration;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.UseDefaultEntityConfig();
        builder.UseDefaultTimeStampsConfig();
        builder.UserDefaultCreatorConfiguration();
    }
}
