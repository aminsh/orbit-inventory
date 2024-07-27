using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_core.application;
using orbit_inventory_core.Auth;
using orbit_inventory_core.Exception;
using orbit_inventory_core.Request;
using orbit_inventory_dto;
using orbit_inventory_security;

namespace orbit_inventory_api;

[Route("v1")]
[Authorize]
public class UserController(
    IOrbitRequestContext requestContext, 
    AuthenticationService authenticationService,
    OrbitAuthenticationService orbitAuthenticationService,
    IUnitOfWork unitOfWork)
{
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task Signup([FromBody] SignupDto dto)
    {
        await authenticationService.Create(dto);
        await unitOfWork.Commit();
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<AuthenticationResponse> Signin([FromBody] SigninDto dto)
    {
        var user = await authenticationService.Signin(dto);
        return orbitAuthenticationService.Create(user);
    }
    
    [HttpGet("me")]
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