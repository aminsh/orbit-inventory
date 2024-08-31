using GraphQL.Types;

namespace orbit_inventory_graphql.types;

public sealed class PageableResponseType<TObjectType> : ObjectGraphType
    where TObjectType : IGraphType
{
    public PageableResponseType()
    {
        Field<ListGraphType<TObjectType>>("data");
        Field<LongGraphType>("count");
    }
}