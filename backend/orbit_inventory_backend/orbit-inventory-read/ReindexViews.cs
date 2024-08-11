using orbit_inventory_core.Domain;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;
using orbit_inventory_read.assembler;

namespace orbit_inventory_read;

public class ReindexViews(ReindexingService reindexingService)
{
    public Task ForProduct()
    {
        return reindexingService.Reindex<ProductView, Product>(ProductAssembler.Assemble);
    }

    public Task ForUser()
    {
        return reindexingService.Reindex<UserView, User>(UserAssembler.Assemble);
    }
}