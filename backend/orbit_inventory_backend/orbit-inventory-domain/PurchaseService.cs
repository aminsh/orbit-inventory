using Microsoft.EntityFrameworkCore;
using orbit_inventory_core.Domain;
using orbit_inventory_core.Exception;

namespace orbit_inventory_domain;

public class PurchaseService(
    IRepository<Purchase> purchaseRepository,
    IRepository<Product> productRepository,
    IRepository<Supplier> supplierRepository,
    InventoryService inventoryService
)
{
    public async Task Create(PurchaseDto dto)
    {
        var supplier = await supplierRepository.FindById(dto.SupplierId);

        if (supplier == null)
            throw new NotFoundException();

        var productIds = dto.Lines.Select(ln => ln.ProductId).ToList();

        var products = await productRepository.Query
            .Where(e => productIds.Contains(e.Id))
            .ToListAsync();

        var entity = new Purchase
        {
            Date = DateTime.Now,
            Supplier = supplier,
            Lines = dto.Lines
                .Join(
                    products, ln => ln.ProductId,
                    p => p.Id,
                    (line, product) => new PurchaseLine
                    {
                        Product = product,
                        Description = line.Description,
                        UnitPrice = line.UnitPrice
                    })
                .ToList(),
        };
        
        inventoryService.CreateMany(entity);
        purchaseRepository.Add(entity);
    }
}