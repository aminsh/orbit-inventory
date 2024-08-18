using orbit_inventory_core.read;

namespace orbit_inventory_dto;

public class CreatedByView : IView
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public interface IHaveCreatedByView
{
    public CreatedByView CreatedBy { get; set; }
}