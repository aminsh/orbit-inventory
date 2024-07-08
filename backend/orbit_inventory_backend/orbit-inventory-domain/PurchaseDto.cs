namespace orbit_inventory_domain;

public class PurchaseDto
{
    public DateTime Date { get; set; }
    public int SupplierId { get; set; }
    public IEnumerable<PurchaseLineDto> Lines { get; set; }
}

public class PurchaseLineDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public double UnitPrice { get; set; }
}