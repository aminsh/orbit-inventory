using orbit_inventory_core.Domain;
using orbit_inventory_core.read;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;

namespace orbit_inventory_read.assembler;

public class ProductViewAssembler(IViewAssembler<User, CreatedByView> createdByViewAssembler)
    : IViewAssembler<Product, ProductView>
{
    public async Task<ProductView> Assemble(Product product)
    {
        var view = new ProductView
        {
            Id = product.Id,
            Name = product.Name,
            Upc = product.Upc,
            CreatedBy = await createdByViewAssembler.Assemble(product.CreatedBy)
        };

        return view;
    }
}