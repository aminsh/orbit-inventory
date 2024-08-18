using Nest;
using orbit_inventory_core.read;
using orbit_inventory_dto;

namespace orbit_inventory_read.mappings;

public static class CreatedByViewObjectMapping
{
    public static IObjectProperty Map<TParent>(ObjectTypeDescriptor<TParent, CreatedByView> objectTypeDescriptor) 
        where TParent : class
    {
        return objectTypeDescriptor
            .Properties(p => p
                .Number(num => num
                    .Name(n => n.Name)
                )
                .Text(tx => tx
                    .Name(n => n.Name)
                )
            );
    }

    public static PropertiesDescriptor<TParent> MapCreatedByProperty<TParent>(
        this PropertiesDescriptor<TParent> propertiesDescriptor) 
        where TParent : class, IView, IHaveCreatedByView
    {
        return propertiesDescriptor
            .Object<CreatedByView>(o => o
                .Name(n => n.CreatedBy)
                .Properties(p => p
                    .Number(num => num
                        .Name(n => n.Name)
                    )
                    .Text(tx => tx
                        .Name(n => n.Name)
                    )
                )
            );
    }
}