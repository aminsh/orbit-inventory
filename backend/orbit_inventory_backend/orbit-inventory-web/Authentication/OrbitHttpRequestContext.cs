using orbit_inventory_core.Type;

namespace orbit_inventory_web.Authentication;

public class OrbitHttpRequestContext: IOrbitRequestContext
{
    public int UserId { get; set; }
}