using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbit_inventory_core.application;
using orbit_inventory_core.Auth;
using orbit_inventory_core.Exception;
using orbit_inventory_core.messaging;
using orbit_inventory_core.read;
using orbit_inventory_core.request;
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
    IReadService readService,
    IEventBus eventBus)
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
        var view = await readService.FindById<UserView>(requestContext.UserId);

        if (view == null)
            throw new NotFoundException();
        
        return view;
    }

    [HttpPut("profile")]
    public async Task Update([FromBody] UserUpdateDto dto)
    {
        await authenticationService.Update(requestContext.UserId, dto);
        await unitOfWork.Commit();
        await eventBus.Send(new UserUpdatedEvent { Id = requestContext.UserId });
    }
}