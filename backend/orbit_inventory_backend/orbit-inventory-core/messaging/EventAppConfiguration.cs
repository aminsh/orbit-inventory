using Microsoft.Extensions.DependencyInjection;
using orbit_inventory_core.Utils;

namespace orbit_inventory_core.messaging;

public static class EventAppConfiguration
{
    public static void AddEvents(this IServiceCollection service)
    {
        MessageMetadataScanner.Scan(
            MessageMetadataCategory.Event,
            typeof(IEvent),
            typeof(IEventHandler<>),
            [
                AssemblyUtils.Get("orbit-inventory-dto"),
                AssemblyUtils.Get("orbit-inventory-read")
            ]
        );

        foreach (var handler in MessageMetadataScanner.GetAllServiceHandlers(MessageMetadataCategory.Event))
            service.AddScoped(handler);

        service.AddScoped<IEventBus, EventBusInMemory>();
    }
}