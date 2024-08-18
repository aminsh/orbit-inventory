using orbit_inventory_core.Domain;
using orbit_inventory_core.read;
using orbit_inventory_dto;

namespace orbit_inventory_read.assembler;

public class CreatedByViewAssembler : IViewAssembler<User, CreatedByView>
{
    public Task<CreatedByView> Assemble(User user)
    {
        var view = new CreatedByView
        {
            Id = user.Id,
            Name = user.Name
        };

        return Task.FromResult(view);
    }
}