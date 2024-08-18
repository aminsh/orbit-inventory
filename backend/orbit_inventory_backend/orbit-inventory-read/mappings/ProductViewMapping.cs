using Nest;
using orbit_inventory_core.read;
using orbit_inventory_dto;

namespace orbit_inventory_read.mappings;

public class ProductViewMapping : IViewMapping<ProductView>
{
    public ITypeMapping Map(TypeMappingDescriptor<ProductView> mappingDescriptor)
    {
        return mappingDescriptor
            .Properties(p => p
                .Number(num => num
                    .Name(n => n.Id)
                    .Type(NumberType.Integer)
                )
                .Text(tx => tx
                    .Name(n => n.Name)
                )
                .Keyword(k => k
                    .Name(n => n.Upc)
                )
                .MapCreatedByProperty()
            );
    }
}