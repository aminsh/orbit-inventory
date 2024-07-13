using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orbit_inventory_core.Data;
using orbit_inventory_core.Domain;

namespace orbit_inventory_data.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .UseDefaultEntityConfig()
            .UseDefaultTimeStampsConfig();
        
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Password).IsRequired();
        builder.Property(p => p.Salt).IsRequired();
    }
}