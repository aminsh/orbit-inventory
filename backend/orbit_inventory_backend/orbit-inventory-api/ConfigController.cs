using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_read;

namespace orbit_inventory_api;

[Route("v1/config")]
[Authorize]
public class ConfigController(ReindexViews reindexViews)
{
    [HttpPost("reindex/product")]
    public Task ReindexProducts()
    {
        return reindexViews.ForProduct();
    }
    
    [HttpPost("reindex/user")]
    public Task ReindexUsers()
    {
        return reindexViews.ForUser();
    }
}