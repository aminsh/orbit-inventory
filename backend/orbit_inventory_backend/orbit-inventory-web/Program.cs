using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using orbit_inventory_application;
using orbit_inventory_core.Type;
using orbit_inventory_data;
using orbit_inventory_domain.Core;
using orbit_inventory_domain.user;
using orbit_inventory_domain.User;
using orbit_inventory_web.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrbitDbContext>(option =>
    option
        .UseNpgsql(
            Environment.GetEnvironmentVariable("PG_CONNECTION")
        )
        .UseSnakeCaseNamingConvention()
);
builder.Services.AddSingleton<IOrbitRequestContext, OrbitHttpRequestContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<UserService>();
builder.Services.AddAuthentication(au =>
{
    au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Authentication_Private_key") ?? string.Empty)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();
builder.Services.AddTransient<OrbitAuthenticationService>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var requestContext = context.RequestServices.GetService<IOrbitRequestContext>();
    requestContext!.UserId = Convert.ToInt32(context.User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value);
    await next.Invoke();
});

app.MapPost("/signup", async (
    UserService userService,
    IUnitOfWork unitOfWork,
    [FromBody] UserDto dto
) =>
{
    await userService.Create(dto);
    await unitOfWork.Commit();
});

app.MapPost("/signin", async (
    UserService userService,
    OrbitAuthenticationService orbitAuthenticationService,
    [FromBody] UserSigninDto dto
) =>
{
    var user = await userService.Signin(dto);
    return orbitAuthenticationService.Create(user);
});

app.MapGet("/me", async (UserService userService, IOrbitRequestContext requestContext) =>
{
    var user = await userService.GetUser(requestContext.UserId);
    return new
    {
        user.Id,
        user.Name,
        user.Email
    };

}).RequireAuthorization();

app.Run();