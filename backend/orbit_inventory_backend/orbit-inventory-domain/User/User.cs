using orbit_inventory_domain.Core;

namespace orbit_inventory_domain.user;

public class User : Entity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}