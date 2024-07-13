namespace orbit_inventory_core.Domain;

public interface IHaveTimestamps
{
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}