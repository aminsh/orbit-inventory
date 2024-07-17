using System.ComponentModel.Design;

namespace orbit_inventory_core.Application;

public interface ICommand
{
    Guid CommandId { get; }
}

public interface ICommand<TResponse> : ICommand
{
}

