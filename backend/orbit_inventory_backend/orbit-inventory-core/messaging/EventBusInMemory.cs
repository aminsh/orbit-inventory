namespace orbit_inventory_core.messaging;

public class EventBusInMemory(IServiceProvider serviceProvider) : IEventBus
{
    public async Task Send<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var handlerTypes = MessageMetadataScanner.GetAllHandlerByMessageType(@event.GetType()).ToList();

        if (!handlerTypes.Any())
            return;

        var tasks = handlerTypes.Select(tp => MessageResolver.Invoke(
            serviceProvider.GetService(tp),
            tp,
            @event,
            mi => mi.Name == "Handle" && mi.GetParameters().First().ParameterType == @event.GetType()
        ));

        await Task.WhenAll(tasks);
    }
}