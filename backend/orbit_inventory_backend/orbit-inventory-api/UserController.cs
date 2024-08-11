using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using orbit_inventory_core.application;
using orbit_inventory_core.Auth;
using orbit_inventory_core.Exception;
using orbit_inventory_core.read;
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
    IUnitOfWork unitOfWork,
    ElasticClient client)
{
    [AllowAnonymous]
    [HttpPost("signUp")]
    public async Task Signup([FromBody] SignupDto dto)
    {
        await authenticationService.Create(dto);
        await unitOfWork.Commit();
    }

    [AllowAnonymous]
    [HttpPost("signIn")]
    public async Task<AuthenticationResponse> Signin([FromBody] SigninDto dto)
    {
        var user = await authenticationService.Signin(dto);
        return orbitAuthenticationService.Create(user);
    }

    [HttpGet("me")]
    public async Task<UserView> Get()
    {
        var response = await client.GetAsync<UserView>(
            requestContext.UserId,
            g => g.Index(ReadHelper.GetIndexNameOf<UserView>()));
        return response.Source;
    }

    [HttpPut("profile")]
    public async Task Update([FromBody] UserUpdateDto dto)
    {
        await authenticationService.Update(requestContext.UserId, dto);
        await unitOfWork.Commit();
    }
}