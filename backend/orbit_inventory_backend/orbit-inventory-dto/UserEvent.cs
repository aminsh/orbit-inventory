using orbit_inventory_core.messaging;

namespace orbit_inventory_dto;

public class UserUpdatedEvent : IEvent
{
    public int Id { get; set; }
}