using orbit_inventory_core.Domain;

namespace orbit_inventory_core.read;

public interface IViewAssembler<in TEntity, TView>
    where TEntity : IEntity
    where TView : IView
{
    public Task<TView> Assemble(TEntity entity);
}