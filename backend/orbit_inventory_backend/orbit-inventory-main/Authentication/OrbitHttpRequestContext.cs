using orbit_inventory_core.Request;

namespace orbit_inventory_main.Authentication;

public class OrbitHttpRequestContext: IOrbitRequestContext
{
    public int UserId { get; set; }
}