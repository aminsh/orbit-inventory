using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orbit_inventory_domain.UserSection;

namespace orbit_inventory_data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Password).IsRequired();
    }
}