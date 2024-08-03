namespace orbit_inventory_core.messaging;

public interface IEvent
{
}

public interface IEventHandler<in TEvent> where TEvent: IEvent
{
    Task Handle(TEvent @event);
}