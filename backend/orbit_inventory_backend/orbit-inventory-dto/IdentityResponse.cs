using orbit_inventory_core.Domain;

namespace orbit_inventory_dto;

public class IdentityResponse
{
    public int Id { get; set; }

    public static IdentityResponse From(IEntity entity)
    {
        return new IdentityResponse
        {
            Id = entity.Id
        };
    }
}