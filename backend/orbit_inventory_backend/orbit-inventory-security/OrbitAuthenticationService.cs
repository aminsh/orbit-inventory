using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using orbit_inventory_core.Domain;
using orbit_inventory_dto;

namespace orbit_inventory_security;

public class OrbitAuthenticationService
{
    public AuthenticationResponse Create(User authenticationUser)
    {
        var handler = new JwtSecurityTokenHandler();
        var privateKey =
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Authentication_Private_key") ?? string.Empty);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddDays(1),
            Subject = GenerateClaims(authenticationUser)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return new AuthenticationResponse
        {
            AccessToken = handler.WriteToken(token),
            TokenType = "Bearer"
        };
    }

    private static ClaimsIdentity GenerateClaims(User authenticationUser)
    {
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim("Id", authenticationUser.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, authenticationUser.Name));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, authenticationUser.Email));

        return claimsIdentity;
    }
}