namespace orbit_inventory_core.messaging;

public class MessageMetadata
{
    public MessageMetadataCategory Category { get; set; }
    public Dictionary<Type, IEnumerable<Type>> TypeHandlers { get; set; }
}

public enum MessageMetadataCategory
{
    Event
}