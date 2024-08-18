using orbit_inventory_core.read;

namespace orbit_inventory_dto;

public class UserView : IView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}