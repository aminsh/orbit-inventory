using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using orbit_inventory_core.auth;
using orbit_inventory_core.Domain;
using orbit_inventory_core.Exception;
using orbit_inventory_core.graphql;
using orbit_inventory_core.read;
using orbit_inventory_core.request;
using orbit_inventory_dto;

namespace orbit_inventory_graphql;

public sealed class UserQuery : ObjectGraphType
{
    public UserQuery()
    {
        Field<NonNullGraphType<UserType>>("authenticatedUser")
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                
                var view = await provider.GetRequiredService<IReadService>()
                    .FindById<UserView>(provider.GetRequiredService<IOrbitRequestContext>().UserId);

                if (view == null)
                    throw new NotFoundException();

                return view;
            })
            .Authorize();
    }
}