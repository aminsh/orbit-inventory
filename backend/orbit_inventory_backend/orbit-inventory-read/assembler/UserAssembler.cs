using orbit_inventory_core.Domain;
using orbit_inventory_core.read;
using orbit_inventory_dto;

namespace orbit_inventory_read.assembler;

public class UserViewAssembler: IViewAssembler<User, UserView>
{
    public Task<UserView> Assemble(User entity)
    {
        var view = new UserView
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email
        };
        
        return Task.FromResult(view);
    }
}