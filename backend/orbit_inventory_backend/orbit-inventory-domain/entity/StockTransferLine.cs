namespace orbit_inventory_domain.entity;

public class StockTransferLine
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public Stock Stock { get; set; }
    public double Quantity { get; set; }
    public string Description { get; set; }
}
