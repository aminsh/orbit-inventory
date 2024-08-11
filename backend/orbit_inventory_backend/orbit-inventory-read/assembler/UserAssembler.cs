using orbit_inventory_core.Domain;
using orbit_inventory_dto;

namespace orbit_inventory_read.assembler;

public class UserAssembler
{
    public static UserView Assemble(User entity)
    {
        return new UserView
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email
        };
    }
}