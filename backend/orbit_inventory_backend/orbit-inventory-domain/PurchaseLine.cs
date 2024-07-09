using orbit_inventory_domain.Core;

namespace orbit_inventory_domain;

public class PurchaseLine : IValueObject
{
    public string Description { get; set; }
    public double UnitPrice { get; set; }

    public ICollection<Inventory> InventoryItems { get; set; }
}