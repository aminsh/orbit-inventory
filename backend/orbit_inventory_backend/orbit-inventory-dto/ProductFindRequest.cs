using orbit_inventory_core.read;

namespace orbit_inventory_dto;

public class ProductFindRequest : IReadPageableRequest
{
    public int Take { get; set; }
    public int Skip { get; set; }
    public string? Name { get; set; }
    public string? Upc { get; set; }
    public int? CreatedById { get; set; }
}