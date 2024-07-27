using System.Reflection;

namespace orbit_inventory_core.Application;

public static class MessageHandlerResolver
{
    public static Type? GetHandler(Type messageType, Type handlerType, Assembly[] assemblies)
    {
        return GetHandlers(messageType, handlerType, assemblies)
            .FirstOrDefault();
    }

    public static IEnumerable<Type> GetHandlers(Type messageType, Type handlerType, Assembly[] assemblies)
    {
        return assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(tp => tp.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == handlerType &&
                          i.GetGenericArguments().First() == messageType
                )
            );
    }
}