using orbit_inventory_core.Domain;
using orbit_inventory_dto;

namespace orbit_inventory_read.assembler;

public static class CreatedByAssembler
{
    public static CreatedByView Assemble(User user)
    {
        return new CreatedByView
        {
            Id = user.Id,
            Name = user.Name
        };
    }
}