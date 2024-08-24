using orbit_inventory_core.Domain;
using orbit_inventory_core.messaging;
using orbit_inventory_core.read;
using orbit_inventory_data;
using orbit_inventory_dto;

namespace orbit_inventory_read.eventHandler;

public class UserEventHandler(
    OrbitDbContext dbContext,
    IReadService readService,
    IViewAssembler<User, UserView> viewAssembler)
    : IEventHandler<UserUpdatedEvent>
{
    public async Task Handle(UserUpdatedEvent @event)
    {
        var entity = await dbContext.Set<User>().FindAsync(@event.Id);

        if (entity == null)
            return;
        
        var view = await viewAssembler.Assemble(entity);

        await readService.Update<UserView>(entity.Id, new
        {
            view.Name, 
            view.Email
        });
    }
}