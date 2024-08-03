using Nest;

namespace orbit_inventory_dto;

public class ProductView
{
    public int Id { get; set; }
    
    [Keyword]
    public string Name { get; set; }
    
    [Keyword]
    public string Upc { get; set; }
    public CreatedByView CreatedBy { get; set; }
}

public class CreatedByView
{
    public int Id { get; set; }
    
    [Keyword]
    public string Name { get; set; }
}