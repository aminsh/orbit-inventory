using orbit_inventory_core.messaging;
using orbit_inventory_core.read;
using orbit_inventory_data;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;

namespace orbit_inventory_read.eventHandler;

public class ProductEventHandlers(
    IReadService readService,
    OrbitDbContext orbitDbContext,
    IViewAssembler<Product, ProductView> viewAssembler)
    : IEventHandler<ProductCreatedEvent>,
        IEventHandler<ProductUpdatedEvent>,
        IEventHandler<ProductDeletedEvent>
{
    public async Task Handle(ProductCreatedEvent createdEvent)
    {
        var entity = await orbitDbContext.Set<Product>().FindAsync(createdEvent.Id);

        if (entity == null)
            return;

        var view = await viewAssembler.Assemble(entity);
        
        await readService.Create(view);
    }

    public async Task Handle(ProductUpdatedEvent @event)
    {
        var entity = await orbitDbContext.Set<Product>().FindAsync(@event.Id);

        if (entity == null)
            return;

        await readService.Update<ProductView>(@event.Id, new
        {
            entity.Name,
            entity.Upc
        });
    }

    public Task Handle(ProductDeletedEvent @event)
    {
        return readService.DeleteById<ProductView>(@event.Id);
    }
}