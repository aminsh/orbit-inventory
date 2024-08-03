using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace orbit_inventory_core.messaging;

public static class MessageResolver
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

    public static IEnumerable<Type> GetMessageTypes(Type baseMessageType, Assembly[] assemblies)
    {
        return assemblies
            .SelectMany(a => a.GetTypes())
            .Where(tp => tp.GetInterfaces().Any(i => i == baseMessageType));
    }

    public static async Task Invoke<TArgs>(
        object? instance,
        Type handlerType,
        TArgs args,
        Func<MethodInfo, bool> methodInfoPredicate
    )
    {
        if (instance == null)
            throw new System.Exception("Service Instance is not valid");

        var method = handlerType.GetMethods().FirstOrDefault(methodInfoPredicate);

        if (method == null)
            throw new System.Exception("Method is not valid");

        await (dynamic)method.Invoke(instance, [args])!;
    }
}