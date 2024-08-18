using Nest;
using orbit_inventory_core.Domain;
using orbit_inventory_core.read;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;
using orbit_inventory_read;
using orbit_inventory_read.assembler;
using orbit_inventory_read.mappings;

namespace orbit_inventory_main.service_configuration;

public static class ReadConfiguration
{
    public static void AddRead(this IServiceCollection service)
    {
        service.AddSingleton<ElasticClient>(_ =>
        {
            var uri = new Uri(Environment.GetEnvironmentVariable("ELASTIC_URL") ?? "");
            return new ElasticClient(uri);
        });

        service.AddTransient<ISearchClient, SearchClient>();
        service.AddTransient<IReadService, ReadService>();
        
        service.AddSingleton<IViewMapping<UserView>, UserViewMapping>();
        service.AddSingleton<IViewMapping<ProductView>, ProductViewMapping>();

        service.AddScoped<IViewAssembler<User, UserView>, UserViewAssembler>();
        service.AddScoped<IViewAssembler<User, CreatedByView>, CreatedByViewAssembler>();
        service.AddScoped<IViewAssembler<Product, ProductView>, ProductViewAssembler>();

        service.AddScoped<IReadPageableRequestResolver<ProductView, ProductFindRequest>, ProductRequestResolver>();
        
        service.AddScoped(typeof(ReIndexingService<,>));
    }
}