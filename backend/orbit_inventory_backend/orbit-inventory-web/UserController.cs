using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_core.Type;
using orbit_inventory_domain.UserSection;

namespace orbit_inventory_web;

[Route("v1/users")]
[Authorize]
public class UserController(IOrbitRequestContext requestContext, UserService userService)
{
    [Route("me")]
    public async Task<object> Get()
    {
        var user = await userService.GetUser(requestContext.UserId);
        return new
        {
            user.Id,
            user.Name,
            user.Email
        };
    }
}