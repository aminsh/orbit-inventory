using GraphQL;
using GraphQL.Types;
using orbit_inventory_core.Utils;
using orbit_inventory_dto;
using orbit_inventory_graphql;

namespace orbit_inventory_main.service_configuration;

public static class GraphqlConfiguration
{
    public static void AddAppGraphql(this IServiceCollection service)
    {
        service.AddGraphQL(b => b
            .AddAuthorizationRule()
            .AddSchema<OrbitSchema>()
            .AddGraphTypes(AssemblyUtils.Get("orbit-inventory-graphql"))
            .AddSystemTextJson()
        );
    }

    public static void UserAppGraphql(this IApplicationBuilder app)
    {
        app.UseGraphQL<ISchema>();
        app.UseGraphQLAltair();
    }
}