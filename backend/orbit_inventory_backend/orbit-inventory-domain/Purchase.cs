using orbit_inventory_domain.Core;

namespace orbit_inventory_domain;

public class Purchase : Entity
{
    public DateTime Date { get; set; }
    public Supplier Supplier { get; set; }
    public ICollection<PurchaseLine> Lines { get; set; }
}