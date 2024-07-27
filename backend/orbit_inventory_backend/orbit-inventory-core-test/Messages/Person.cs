using orbit_inventory_core.Application;

namespace orbit_inventory_core_test;

public class Person : ICommand<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid CommandId { get; set; }
}

public class PersonHandler : ICommandHandler<Person, int>
{
    public Task<int> Handle(Person command)
    {
        Console.Write(command.ToString());
        return Task.FromResult(100);
    }
}