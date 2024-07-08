using orbit_inventory_domain.Core;

namespace orbit_inventory_domain;

public class Inventory : Entity
{
    public Product Product { get; set; }
    public Supplier Supplier { get; set; }
    public bool? Available { get; set; }
}
