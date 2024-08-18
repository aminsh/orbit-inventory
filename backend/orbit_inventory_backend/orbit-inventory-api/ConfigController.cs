using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_core.Domain;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;
using orbit_inventory_read;

namespace orbit_inventory_api;

[Route("v1/config")]
[Authorize]
public class ConfigController(
    ReIndexingService<User, UserView> reIndexingUser,
    ReIndexingService<Product, ProductView> reIndexingProduct)
{
    [HttpPost("reIndex")]
    public async Task ReindexAll()
    {
       await Task.WhenAll(reIndexingUser.ReIndex(), reIndexingProduct.ReIndex());
    }
    
    [HttpPost("reIndex/product")]
    public Task ReindexProducts()
    {
        return reIndexingProduct.ReIndex();
    }
    
    [HttpPost("reIndex/user")]
    public Task ReindexUsers()
    {
        return reIndexingUser.ReIndex();
    }
}