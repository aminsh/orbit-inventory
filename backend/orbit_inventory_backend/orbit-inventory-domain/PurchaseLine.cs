namespace orbit_inventory_domain;

public class PurchaseLine
{
    public int Id { get; set; }
    public virtual Product Product { get; set; }
    public string Description { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public virtual ICollection<Inventory>? Items { get; set; }
}