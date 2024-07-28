using orbit_inventory_core.messaging;
using orbit_inventory_core.Request;
using orbit_inventory_main.service_configuration;
using orbit_inventory_read;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddShared();
builder.Services.AddSecurity();
builder.Services.AddDomain();
builder.Services.AddEvents();

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

app.MapPost("/elasticCofigure", async (
    ProductViewConfiguration productViewConfiguration
) =>
{
    await productViewConfiguration.Configure();
});

app.Run();

