using orbit_inventory_domain.Core;

namespace orbit_inventory_domain;

public class PurchaseLine : IValueObject
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public double UnitPrice { get; set; }
}