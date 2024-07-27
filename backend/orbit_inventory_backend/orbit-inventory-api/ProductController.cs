using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_core.application;
using orbit_inventory_domain.service;
using orbit_inventory_dto;

namespace orbit_inventory_api;

[Authorize]
[Route("v1/products")]
public class ProductController(IUnitOfWork unitOfWork, ProductService productService)
{
    [HttpPost]
    public async Task<IdentityResponse> Create([FromBody] ProductDto dto)
    {
        var entity = await productService.Create(dto);
        await unitOfWork.Commit();
        return IdentityResponse.From(entity);
    }
    
    [HttpPut("{id:int}")]
    public async Task Update([FromRoute] int id, [FromBody] ProductDto dto)
    {
        await productService.Update(id, dto);
        await unitOfWork.Commit();
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete([FromRoute] int id)
    {
        await productService.Delete(id);
        await unitOfWork.Commit();
    }
}