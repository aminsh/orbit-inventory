using System.Security.Claims;

namespace orbit_inventory_core.auth;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Identity is { IsAuthenticated: false } 
            ? 0 
            : Convert.ToInt32(claimsPrincipal.Claims.First(cl => cl.Type == "Id").Value);
    }
}