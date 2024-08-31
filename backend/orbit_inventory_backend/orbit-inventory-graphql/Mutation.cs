using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using orbit_inventory_core.application;
using orbit_inventory_core.auth;
using orbit_inventory_core.graphql;
using orbit_inventory_core.messaging;
using orbit_inventory_core.request;
using orbit_inventory_domain.service;
using orbit_inventory_dto;
using orbit_inventory_graphql.types;
using orbit_inventory_security;

namespace orbit_inventory_graphql;

public sealed class Mutation : ObjectGraphType
{
    public Mutation()
    {
        Field<VoidType>("signUp")
            .Arguments(new QueryArguments(
                new QueryArgument<SignUpInputType> { Name = "input" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                await provider.GetRequiredService<AuthenticationService>()
                    .Create(context.GetArgument<SignUpDto>("input"));
                await provider.GetRequiredService<IUnitOfWork>().Commit();
                return null;
            });

        Field<AuthenticationResponseType>("signIn")
            .Arguments(new QueryArguments(
                new QueryArgument<SignInInputType> { Name = "input" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                var user = await provider.GetRequiredService<AuthenticationService>()
                    .Signin(context.GetArgument<SignInDto>("input"));
                return provider.GetRequiredService<OrbitAuthenticationService>().Create(user);
            });

        Field<VoidType>("updateProfile")
            .Arguments(new QueryArguments(
                new QueryArgument<UpdateProfileInputType> { Name = "input" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                var id = provider.GetRequiredService<IOrbitRequestContext>().UserId;

                await provider.GetRequiredService<AuthenticationService>().Update(
                    id,
                    context.GetArgument<UserUpdateDto>("input")
                );
                await provider.GetRequiredService<IUnitOfWork>().Commit();
                await provider.GetRequiredService<IEventBus>().Send(new UserUpdatedEvent { Id = id });

                return null;
            })
            .Authorize();
        
        Field<IdGraphType>("createProduct")
            .Arguments(new QueryArguments(
                new QueryArgument<ProductInput> { Name = "input" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                var result = await provider.GetRequiredService<ProductService>()
                    .Create(context.GetArgument<ProductDto>("input"));
                await provider.GetRequiredService<IUnitOfWork>().Commit();
                await provider.GetRequiredService<IEventBus>().Send(new ProductCreatedEvent { Id = result.Id });
                return result.Id;
            })
            .Authorize();

        Field<VoidType>("updateProduct")
            .Arguments(new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" },
                new QueryArgument<ProductInput> { Name = "input" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                var id = context.GetArgument<int>("id");
                await provider.GetRequiredService<ProductService>()
                    .Update(id, context.GetArgument<ProductDto>("input"));
                await provider.GetRequiredService<IUnitOfWork>().Commit();
                await provider.GetRequiredService<IEventBus>().Send(new ProductUpdatedEvent { Id = id });
                return null;
            })
            .Authorize();

        Field<VoidType>("deleteProduct")
            .Arguments(new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" }
            ))
            .ResolveAsync(async context =>
            {
                var provider = context.Scope().ServiceProvider;
                var id = context.GetArgument<int>("id");
                await provider.GetRequiredService<ProductService>()
                    .Delete(id);
                await provider.GetRequiredService<IUnitOfWork>().Commit();
                await provider.GetRequiredService<IEventBus>().Send(new ProductDeletedEvent { Id = id });
                return null;
            })
            .Authorize();
    }
}