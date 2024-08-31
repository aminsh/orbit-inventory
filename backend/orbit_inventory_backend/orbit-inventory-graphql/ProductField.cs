using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace orbit_inventory_graphql;

public class ProductField
{
    public ProductField()
    {
        FieldType field = new FieldType
        {
            Name = "findProductById",
            Type = typeof(ProductType),
            Arguments = new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ),
        };
        
        var root = new ObjectGraphType { Name = "Root" };


        /*Field<NonNullGraphType<ProductType>>("findProductById")
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
            .Authorize();*/
        
    }
}