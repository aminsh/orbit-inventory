using FluentValidation;
using FluentValidation.AspNetCore;
using orbit_inventory_core.messaging;
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
builder.Services.AddAppGraphql();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("MyPolicy");
app.UserAppGraphql();

app.Run();