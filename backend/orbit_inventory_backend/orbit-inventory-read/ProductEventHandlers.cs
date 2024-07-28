using Nest;
using orbit_inventory_core.messaging;
using orbit_inventory_data;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;
using orbit_inventory_read.assembler;

namespace orbit_inventory_read;

public class ProductEventHandlers(
    OrbitDbContext orbitDbContext,
    ElasticClient elasticClient
) :
    IEventHandler<ProductCreatedEvent>,
    IEventHandler<ProductUpdatedEvent>,
    IEventHandler<ProductDeletedEvent>
{
    public async Task Handle(ProductCreatedEvent @event)
    {
        var entity = await orbitDbContext.Set<Product>().FindAsync(@event.Id);

        if (entity == null)
            return;

        await elasticClient.IndexAsync(
            ProductAssembler.Assemble(entity),
            x => x.Index("orbit-product-view")
        );
    }

    public async Task Handle(ProductUpdatedEvent @event)
    {
        var entity = await orbitDbContext.Set<Product>().FindAsync(@event.Id);

        if (entity == null)
            return;

        await elasticClient.IndexAsync(
            ProductAssembler.Assemble(entity),
            x => x.Index("orbit-product-view")
        );
    }

    public async Task Handle(ProductDeletedEvent @event)
    {
        await elasticClient.DeleteAsync(new DeleteRequest("orbit-product-view", @event.Id));
    }
}