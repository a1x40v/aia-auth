using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AiaAuth.Services;

public class AuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateToken(string userId, string userEmail)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, userEmail)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSecret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
