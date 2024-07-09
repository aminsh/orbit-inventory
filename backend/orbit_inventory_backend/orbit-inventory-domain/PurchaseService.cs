using Microsoft.EntityFrameworkCore;
using orbit_inventory_core.Type;
using orbit_inventory_domain.Core;
using orbit_inventory_domain.UserSection;

namespace orbit_inventory_domain;

public class PurchaseService(
    IOrbitRequestContext orbitRequestContext,
    IGeneralRepository<User, int> userRepository,
    IEntityRepository<Purchase> purchaseRepository,
    IEntityRepository<Product> productRepository,
    IEntityRepository<Supplier> supplierRepository
)
{
    public async Task Create(PurchaseDto dto)
    {
        var user = await userRepository.FindById(orbitRequestContext.UserId);
        var supplier = await supplierRepository.FindById(dto.SupplierId);
        
        var products = await productRepository.Find()
            .Where(e => dto.Lines.Select(ln => ln.ProductId).Contains(e.Id))
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
                        Description = line.Description,
                        UnitPrice = line.UnitPrice
                    })
                .ToList(),
        };
    }
}