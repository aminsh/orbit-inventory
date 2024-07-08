using orbit_inventory_domain.UserSection;

namespace orbit_inventory_domain.Core;

public class Entity
{
    public int Id { get; set; }
    public User CreatedBy { get; set; }
}