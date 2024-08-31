using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using orbit_inventory_core.Exception;
using orbit_inventory_core.graphql;
using orbit_inventory_core.read;
using orbit_inventory_core.request;
using orbit_inventory_dto;
using orbit_inventory_graphql.types;

namespace orbit_inventory_graphql;

public sealed class Query : ObjectGraphType
{
    public Query()
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
        
        Field<NonNullGraphType<ProductType>>("findProductById")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                var view = await provider.GetRequiredService<IReadService>()
                    .FindById<ProductView>(context.GetArgument<int>("id"));

                if (view == null)
                    throw new NotFoundException();

                return view;
            })
            .Authorize();

        Field<NonNullGraphType<PageableResponseType<ProductType>>>("findProducts")
            .Arguments(new QueryArguments(
                new QueryArgument<ProductFindInput> { Name = "request" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                var res = await provider.GetRequiredService<IReadService>()
                    .Find<ProductView, ProductFindRequest>(context.GetArgument<ProductFindRequest>("request"));
                return res;
            })
            .Authorize();
    }
}