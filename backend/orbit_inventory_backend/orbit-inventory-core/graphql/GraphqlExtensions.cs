using GraphQL;
using Microsoft.Extensions.DependencyInjection;
using orbit_inventory_core.auth;
using orbit_inventory_core.request;

namespace orbit_inventory_core.graphql;

public static class GraphqlExtensions
{
    public static IServiceScope Scope(this IResolveFieldContext<object?> resolveFiledContext)
    {
        if (resolveFiledContext.RequestServices == null)
            return default!;

        var scope = resolveFiledContext.RequestServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<IOrbitRequestContext>().UserId =
            resolveFiledContext.User?.GetUserId() ?? 0;

        return scope;
    }
}