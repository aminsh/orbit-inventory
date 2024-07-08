using orbit_inventory_domain.Core;

namespace orbit_inventory_domain;

public class Product : Entity
{
    public string Name { get; set; }
    public string Upc { get; set; }
}