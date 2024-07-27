using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using orbit_inventory_core.application;
using orbit_inventory_core.Domain;
using orbit_inventory_core.Request;
using orbit_inventory_data;
using orbit_inventory_main.Authentication;

namespace orbit_inventory_main.service_configuration;

public static class SharedConfiguration
{
    public static void AddShared(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        service.AddDbContext<OrbitDbContext>(option =>
            option
                .UseNpgsql(
                    Environment.GetEnvironmentVariable("PG_CONNECTION")
                )
                .UseLazyLoadingProxies()
                .UseSnakeCaseNamingConvention()
        );
        service.AddSingleton<IOrbitRequestContext, OrbitHttpRequestContext>();
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "JWTToken_Auth_API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    public static void UseShared(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}