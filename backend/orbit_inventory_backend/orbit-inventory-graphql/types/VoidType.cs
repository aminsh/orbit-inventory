using GraphQL.Types;
using GraphQLParser.AST;

namespace orbit_inventory_graphql.types;

public class VoidType : ScalarGraphType
{
    public VoidType()
    {
        Name = "Void";
    }
    
    public override object? ParseLiteral(GraphQLValue value)
    {
        return null;
    }

    public override object? ParseValue(object? value)
    {
        return null;
    }

    public override object? Serialize(object? value)
    {
        return null;
    }
}