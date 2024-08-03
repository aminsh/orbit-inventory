using orbit_inventory_core.Domain;

namespace orbit_inventory_domain.entity;

public class Purchase : IEntity, IHaveCreator, IHaveTimestamps
{
    public int Id { get; set; }
    public virtual User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime Date { get; set; }
    public virtual Supplier Supplier { get; set; }
    public PurchaseStatus Status { get; set; }
    public virtual ICollection<PurchaseLine> Lines { get; set; }
}

public enum PurchaseStatus
{
    Draft,
    Fixed
}