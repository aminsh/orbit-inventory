using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace orbit_inventory_graphql;

public class UserSchema : Schema
{
    public UserSchema(IServiceProvider serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<UserQuery>();
        Mutation = serviceProvider.GetRequiredService<UserMutation>();
    }
}