namespace orbit_inventory_core.Domain;

public interface IHaveCreator
{
    User CreatedBy { get; set; }
}