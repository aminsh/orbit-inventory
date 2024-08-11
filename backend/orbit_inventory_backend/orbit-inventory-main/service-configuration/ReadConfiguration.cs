using orbit_inventory_read;

namespace orbit_inventory_main.service_configuration;

public static class ReadConfiguration
{
    public static void AddRead(this IServiceCollection service)
    {
        service.AddScoped<ReindexingService>();
        service.AddScoped<ReindexViews>();
    }
}