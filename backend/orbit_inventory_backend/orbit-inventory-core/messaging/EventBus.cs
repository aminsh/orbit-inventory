namespace orbit_inventory_core.messaging;

public interface IEventBus
{
    Task Send<TEvent>(TEvent @event) where TEvent: IEvent;
}