using Nest;

namespace orbit_inventory_core.read;

public interface IViewMapping<TView>
    where TView : class, IView
{
    public ITypeMapping Map(TypeMappingDescriptor<TView> mappingDescriptor);
}