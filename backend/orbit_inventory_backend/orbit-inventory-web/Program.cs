using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using orbit_inventory_application;
using orbit_inventory_core.Auth;
using orbit_inventory_core.Data;
using orbit_inventory_core.Domain;
using orbit_inventory_core.Exception;
using orbit_inventory_core.Request;
using orbit_inventory_data;
using orbit_inventory_web.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<OrbitDbContext>(option =>
    option
        .UseNpgsql(
            Environment.GetEnvironmentVariable("PG_CONNECTION")
        )
        .UseLazyLoadingProxies()
        .UseSnakeCaseNamingConvention()
);
builder.Services.AddSingleton<IOrbitRequestContext, OrbitHttpRequestContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<AuthenticationService>();
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
app.MapControllers();

app.Use(async (context, next) =>
{
    if (context.User.Identity is { IsAuthenticated: true })
    {
        var requestContext = context.RequestServices.GetService<IOrbitRequestContext>();
        
        requestContext!.UserId = Convert.ToInt32(context.User.Claims.First(cl => cl.Type == "Id").Value);
    }
    
    await next.Invoke();
});

app.MapPost("/signup", async (
    AuthenticationService userService,
    IUnitOfWork unitOfWork,
    [FromBody] SignupDto dto
) =>
{
    await userService.Create(dto);
    await unitOfWork.Commit();
});

app.MapPost("/signin", async (
    AuthenticationService userService,
    OrbitAuthenticationService orbitAuthenticationService,
    [FromBody] SigninDto dto
) =>
{
    var user = await userService.Signin(dto);
    return orbitAuthenticationService.Create(user);
});

app.MapGet("/me", async (AuthenticationService userService, IOrbitRequestContext requestContext) =>
{
    var user = await userService.GetUser(requestContext.UserId);

    if (user == null)
        throw new NotFoundException();
    
    return new
    {
        user.Id,
        user.Name,
        user.Email
    };

}).RequireAuthorization();

app.Run();