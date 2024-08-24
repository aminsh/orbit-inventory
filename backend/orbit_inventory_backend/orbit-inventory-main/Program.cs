using FluentValidation;
using FluentValidation.AspNetCore;
using orbit_inventory_core.messaging;
using orbit_inventory_core.request;
using orbit_inventory_core.Utils;
using orbit_inventory_main.filters;
using orbit_inventory_main.service_configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", build =>
{
    build
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<ExceptionHandlerFilter>();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(AssemblyUtils.Get("orbit-inventory-dto"));

builder.Services.AddShared();
builder.Services.AddSecurity();
builder.Services.AddDomain();
builder.Services.AddEvents();
builder.Services.AddRead();

var app = builder.Build();
app.UseShared();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("MyPolicy");

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