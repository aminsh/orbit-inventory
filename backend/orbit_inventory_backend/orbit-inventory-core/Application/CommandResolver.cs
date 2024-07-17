using orbit_inventory_core.Domain;

namespace orbit_inventory_core.Application;

public static class CommandResolver
{
    public static Type TypeOf<TCommand>(this TCommand command) where TCommand : ICommand
    {   
        
    }
}