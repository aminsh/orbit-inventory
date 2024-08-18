namespace orbit_inventory_core_test;

public class Person
{
    public int Amount { get; set; }
}

public class Sku
{
    public int Id { get; set; }
    public int Sold { get; set; }
    public string Slug { get; set; }

    public int country_id { get; set; }
}