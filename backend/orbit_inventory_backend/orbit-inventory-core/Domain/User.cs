namespace orbit_inventory_core.Domain;

public class User : IEntity, IHaveTimestamps
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}