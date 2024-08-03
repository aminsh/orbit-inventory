using System.Reflection;

namespace orbit_inventory_core.messaging;

public static class MessageMetadataScanner
{
    private static IList<MessageMetadata> _messageMetadata = new List<MessageMetadata>();

    public static void Scan(
        MessageMetadataCategory category,
        Type messageInterfaceType,
        Type messageHandlerInterfaceType,
        Assembly[] assemblies)
    {
        var messageTypes = MessageResolver.GetMessageTypes(messageInterfaceType, assemblies);
        _messageMetadata.Add(new MessageMetadata
        {
            Category = category,
            TypeHandlers = messageTypes
                .Select(tp => new
                {
                    Type = tp,
                    Handlers = MessageResolver.GetHandlers(tp, messageHandlerInterfaceType, assemblies)
                })
                .ToDictionary(mt => mt.Type, mht => mht.Handlers)
        });
    }

    public static IEnumerable<Type> GetAllServiceHandlers()
    {
        return _messageMetadata
            .SelectMany(md => md.TypeHandlers.Values
                .SelectMany(v => v))
            .Distinct();
    }

    public static IEnumerable<Type> GetAllHandlerByMessageType(Type messageType)
    {
        return 
            _messageMetadata
                .SelectMany(md => md.TypeHandlers)
                .FirstOrDefault(th => th.Key == messageType).Value;
    }
}