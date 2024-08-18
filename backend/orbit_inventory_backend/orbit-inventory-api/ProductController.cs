using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_core.application;
using orbit_inventory_core.Exception;
using orbit_inventory_core.messaging;
using orbit_inventory_core.read;
using orbit_inventory_domain.service;
using orbit_inventory_dto;

namespace orbit_inventory_api;

[Authorize]
[Route("v1/products")]
public class ProductController(
    IUnitOfWork unitOfWork,
    ProductService productService,
    IEventBus eventBus,
    IReadService readService)
{
    [HttpPost]
    public async Task<IdentityResponse> Create([FromBody] ProductDto dto)
    {
        var entity = await productService.Create(dto);
        await unitOfWork.Commit();
        await eventBus.Send(new ProductCreatedEvent { Id = entity.Id });
        return IdentityResponse.From(entity);
    }

    [HttpPut("{id:int}")]
    public async Task Update([FromRoute] int id, [FromBody] ProductDto dto)
    {
        await productService.Update(id, dto);
        await eventBus.Send(new ProductUpdatedEvent { Id = id });
        await unitOfWork.Commit();
    }

    [HttpDelete("{id:int}")]
    public async Task Delete([FromRoute] int id)
    {
        await productService.Delete(id);
        await eventBus.Send(new ProductDeletedEvent { Id = id });
        await unitOfWork.Commit();
    }

    [HttpGet("{id:int}")]
    public async Task<ProductView?> GetById([FromRoute] int id)
    {
        var view = await
            readService.FindById<ProductView>(id);

        if (view == null)
            throw new NotFoundException();

        return view;
    }

    [HttpGet]
    public Task<IReadPageableResponse<ProductView>> Get([FromQuery] ProductFindRequest request)
    {
        return readService.Find<ProductView, ProductFindRequest>(request);
    }
}