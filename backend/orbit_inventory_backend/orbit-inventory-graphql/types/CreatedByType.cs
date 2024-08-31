using GraphQL.Types;
using orbit_inventory_dto;

namespace orbit_inventory_graphql.types;

public class CreatedByType : AutoRegisteringObjectGraphType<CreatedByView>
{
    public CreatedByType()
    {
        Name = "CreatedBy";
    }
}