using orbit_inventory_domain.UserSection;

namespace orbit_inventory_domain;

public class InventoryService
{
    public async Task<IEnumerable<Inventory>> CreateMany(
        Supplier supplier,
        User creator,
        IEnumerable<IInventoryCreateMany> items
    )
    {
        return items
            .Select(it => Enumerable.Range(0, it.Quantity).Select(_ => new Inventory
            {
                Supplier = supplier,
                CreatedBy = creator,
                Product = it.Product,
                Available = null
            }))
            .SelectMany(it => it)
            .ToList();
    }
}

public interface IInventoryCreateMany
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}