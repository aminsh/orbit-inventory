using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using orbit_inventory_core.auth;
using orbit_inventory_security;

namespace orbit_inventory_main.service_configuration;

public static class AuthConfiguration
{
    public static void AddSecurity(this IServiceCollection service)
    {
        service.AddScoped<AuthenticationService>();
        service.AddAuthentication(au =>
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
        service.AddAuthorization();
        service.AddTransient<OrbitAuthenticationService>();
    }
}