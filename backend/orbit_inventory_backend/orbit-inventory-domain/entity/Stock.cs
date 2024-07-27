using orbit_inventory_core.Domain;

namespace orbit_inventory_domain.entity;

public class Stock: IEntity, IHaveCreator, IHaveTimestamps
{
    public int Id { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; }
}