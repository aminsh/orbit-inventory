using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace orbit_inventory_graphql;

public class OrbitSchema : Schema
{
    public OrbitSchema(IServiceProvider serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<Query>(); 
        Mutation = serviceProvider.GetRequiredService<Mutation>();
    }
}