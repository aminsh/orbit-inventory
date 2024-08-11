using Nest;

namespace orbit_inventory_dto;

public class UserView
{
    public int Id { get; set; }

    [Keyword] public string Name { get; set; }

    [Keyword] public string Email { get; set; }
}