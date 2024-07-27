using System.Reflection;
using orbit_inventory_core.Application;
using orbit_inventory_core.Utils;

namespace orbit_inventory_core_test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var handlerType = MessageHandlerResolver.GetHandler(
            typeof(Person),
            typeof(ICommandHandler<,>),
            [AssemblyUtils.Get("orbit-inventory-core-test")]);

        if (handlerType == null)
            throw new Exception();

        var instance = Activator.CreateInstance(handlerType);
        var method = handlerType.GetMethod("Handle");
        var message = new Person { FirstName = "Amin", LastName = "Sheikhi", CommandId = Guid.NewGuid() };

        if (method == null)
            throw new Exception();

        var result = method.Invoke(instance, [message])!;
       // var result = await (dynamic)method.Invoke(instance, [message])!;
        
        Console.WriteLine(result);
    }
}