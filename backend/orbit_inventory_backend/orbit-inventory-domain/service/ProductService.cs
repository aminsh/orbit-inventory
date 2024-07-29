using Microsoft.EntityFrameworkCore;
using orbit_inventory_core.Domain;
using orbit_inventory_core.Exception;
using orbit_inventory_core.messaging;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;

namespace orbit_inventory_domain.service;

public class ProductService(
    IRepository<Product> productRepository,
    IEventBus eventBus
)
{
    public async Task<Product> Create(ProductDto dto)
    {
        var isUpcDuplicated = await productRepository.Query.AnyAsync(p => p.Upc == dto.Upc);

        if (isUpcDuplicated)
            throw new BadRequestException("");

        var entity = new Product
        {
            Upc = dto.Upc,
            Name = dto.Name
        };

        productRepository.Add(entity);

        await eventBus.Send(new ProductCreatedEvent { Id = entity.Id });
        return entity;
    }

    public async Task Update(int id, ProductDto dto)
    {
        var entity = await productRepository.FindById(id);

        if (entity == null)
            throw new NotFoundException();

        var isUpcDuplicated = await productRepository.Query.AnyAsync(p => p.Upc == dto.Upc && p.Id != id);

        if (isUpcDuplicated)
            throw new BadRequestException("the_upc_is_duplicated");

        entity.Upc = dto.Upc;
        entity.Name = dto.Name;
    }

    public async Task Delete(int id)
    {
        var entity = await productRepository.FindById(id);

        if (entity == null)
            throw new NotFoundException();

        productRepository.Remove(entity);
    }
}