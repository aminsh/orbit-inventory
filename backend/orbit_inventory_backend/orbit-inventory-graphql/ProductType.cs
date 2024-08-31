using GraphQL.Types;
using orbit_inventory_dto;
using orbit_inventory_graphql.types;

namespace orbit_inventory_graphql;

public sealed class ProductType : ObjectGraphType<ProductView>
{
    public ProductType()
    {
        Field(f => f.Id);
        Field(f => f.Name);
        Field(f => f.Upc);
        Field(f => f.CreatedBy, typeof(CreatedByType))
            .Resolve(context => context.Source.CreatedBy);
    }
}

public class ProductInput : AutoRegisteringInputObjectGraphType<ProductDto>
{
    public ProductInput()
    {
        Name = nameof(ProductInput);
    }
}

public class ProductFindInput : AutoRegisteringInputObjectGraphType<ProductFindRequest>
{
    public ProductFindInput()
    {
        Name = nameof(ProductFindInput);
    }
}