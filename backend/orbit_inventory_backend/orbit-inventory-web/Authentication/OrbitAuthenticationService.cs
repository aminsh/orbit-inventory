using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using orbit_inventory_domain.UserSection;

namespace orbit_inventory_web.Authentication;

public class OrbitAuthenticationService
{
    public string Create(User user)
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
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
    
    private static ClaimsIdentity GenerateClaims(User user)
    {
        var claimsIdentity = new ClaimsIdentity();
        
        claimsIdentity.AddClaim(new Claim("Id", user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        return claimsIdentity;
    }
}