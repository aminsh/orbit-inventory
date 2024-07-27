using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orbit_inventory_domain;
using orbit_inventory_domain.entity;

namespace orbit_inventory_data.Configuration;

public class PurchaseLineEntityConfiguration : IEntityTypeConfiguration<PurchaseLine>
{
    public void Configure(EntityTypeBuilder<PurchaseLine> builder)
    {
        builder.HasOne(p => p.Product).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}