using orbit_inventory_core.Request;
using orbit_inventory_main.service_configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddShared();
builder.Services.AddSecurity();
builder.Services.AddDomain();

var app = builder.Build();
app.UseShared();
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

app.Run();