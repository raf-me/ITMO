using IdentityService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Services;

public interface IJWTTokenService
{
    string GenerateAccessToken(User user);
    string GenerateTemporaryToken(Guid userId);
    ClaimsPrincipal? ValidateTemporaryToken(string token);
}

public class JWTTokenService : IJWTTokenService
{
    private readonly IConfiguration _config;

    public JWTTokenService(IConfiguration config)
    {
        _config = config;
    }

    private SymmetricSecurityKey Key =>
        new(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));

    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim("status", user.Status.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpirationMinutes"]!)),
            signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateTemporaryToken(Guid userId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim("token_type", "2fa")
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:TemporaryTokenExpirationMinutes"]!)),
            signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateTemporaryToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler { MapInboundClaims = false };
            var principal = handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Key,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return principal.FindFirstValue("token_type") == "2fa" ? principal : null;
        }
        catch
        {
            return null;
        }
    }
}
