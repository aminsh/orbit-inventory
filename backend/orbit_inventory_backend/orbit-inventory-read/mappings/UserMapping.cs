using Nest;
using orbit_inventory_core.read;
using orbit_inventory_dto;

namespace orbit_inventory_read.mappings;

public class UserViewMapping : IViewMapping<UserView>
{
    public ITypeMapping Map(TypeMappingDescriptor<UserView> mappingDescriptor)
    {
        return mappingDescriptor
            .Properties(p => p
                .Number(num => num
                    .Name(n => n.Name)
                )
                .Wildcard(wc => wc
                    .Name(n => n.Email)
                )
                .Text(tx => tx
                    .Name(n => n.Name)
                )
            );
    }
}