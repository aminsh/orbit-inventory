using orbit_inventory_domain.entity;
using orbit_inventory_dto;

namespace orbit_inventory_read.assembler;

public class ProductAssembler
{
    public static ProductView Assemble(Product product)
    {
        return new ProductView
        {
            Id = product.Id,
            Name = product.Name,
            Upc = product.Upc,
            CreatedBy = CreatedByAssembler.Assemble(product.CreatedBy)
        };
    }
}