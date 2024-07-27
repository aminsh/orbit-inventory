using orbit_inventory_core.Domain;

namespace orbit_inventory_domain.entity;

public class Product : IEntity, IHaveCreator, IHaveTimestamps
{
    public int Id { get; set; }
    public virtual User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Upc { get; set; }
}