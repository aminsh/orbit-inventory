namespace orbit_inventory_domain;

public class InventoryService
{
    public void CreateMany(Purchase purchase)
    {
        foreach (var line in purchase.Lines)
        {
            line.Items = Enumerable.Range(0, line.Quantity).Select(_ => new Inventory
            {
                Supplier = purchase.Supplier,
                Product = line.Product,
                Available = null
            }).ToList();
        }
    }
}
