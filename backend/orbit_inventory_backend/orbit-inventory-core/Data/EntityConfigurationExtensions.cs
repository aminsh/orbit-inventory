using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using orbit_inventory_core.Domain;

namespace orbit_inventory_core.Data;

public static class EntityConfigurationExtensions
{
    public static EntityTypeBuilder<TEntity> UseDefaultEntityConfig<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
    {
        builder.HasKey(p => p.Id);
        return builder;
    }

    public static EntityTypeBuilder<TCreator> UserDefaultCreatorConfiguration<TCreator>(this EntityTypeBuilder<TCreator> builder) where TCreator: class, IHaveCreator
    {
        builder
            .HasOne(p => p.CreatedBy)
            .WithMany();
        return builder;
    }

    public static EntityTypeBuilder<TTimestamps> UseDefaultTimeStampsConfig<TTimestamps>(this EntityTypeBuilder<TTimestamps> builder) where TTimestamps: class, IHaveTimestamps
    {
        builder
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAdd();

        builder
            .Property(p => p.UpdatedAt)
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAddOrUpdate();
        
        return builder;
    }
}