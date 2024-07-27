using orbit_inventory_domain.service;

namespace orbit_inventory_main.service_configuration;

public static class DomainConfiguration
{
    public static void AddDomain(this IServiceCollection service)
    {
        service.AddScoped<ProductService>();
    }
}