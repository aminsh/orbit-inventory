using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_application;
using orbit_inventory_core.Auth;
using orbit_inventory_core.Exception;
using orbit_inventory_core.Type;
using orbit_inventory_web.Authentication;

namespace orbit_inventory_web;

[Route("v1")]
public class UserController(
    IOrbitRequestContext requestContext, 
    AuthenticationService authenticationService,
    OrbitAuthenticationService orbitAuthenticationService,
    IUnitOfWork unitOfWork)
{
    [Route("signup")]
    public async Task Signup([FromBody] SignupDto dto)
    {
        await authenticationService.Create(dto);
        await unitOfWork.Commit();
    }

    [Route("signin")]
    public async Task<string> Signin([FromBody] SigninDto dto)
    {
        var user = await authenticationService.Signin(dto);
        return orbitAuthenticationService.Create(user);
    }
    
    [Route("me")]
    [Authorize]
    public async Task<object> Get()
    {
        var user = await authenticationService.GetUser(requestContext.UserId);

        if (user == null)
            throw new NotFoundException();
        
        return new
        {
            user.Id,
            user.Name,
            user.Email
        };
    }
}