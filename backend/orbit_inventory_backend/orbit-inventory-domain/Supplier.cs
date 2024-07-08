using orbit_inventory_domain.Core;

namespace orbit_inventory_domain;

public class Supplier : Entity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}