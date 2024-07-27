namespace orbit_inventory_core.Domain;

public interface IValueObject
{
    public bool IsDeleted { get; set; }
}

public static class ValueObjectExtensions
{
    public static void Delete(this IValueObject valueObject)
    {
        valueObject.IsDeleted = true;
    }
}