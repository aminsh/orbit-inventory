using orbit_inventory_core.Domain;

namespace orbit_inventory_domain;

public class Inventory : IEntity, IHaveCreator, IHaveTimestamps
{
    public int Id { get; set; }
    public virtual User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual Product Product { get; set; }
    public virtual Supplier Supplier { get; set; }
    public bool? Available { get; set; }
}
