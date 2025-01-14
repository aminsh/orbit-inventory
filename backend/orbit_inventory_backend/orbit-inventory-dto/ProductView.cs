using orbit_inventory_core.read;

namespace orbit_inventory_dto;

public class ProductView : IView, IHaveCreatedByView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Upc { get; set; }
    public CreatedByView CreatedBy { get; set; }
}