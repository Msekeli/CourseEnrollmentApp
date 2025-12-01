using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;   // ← REQUIRED
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseEnrollmentApp.Application.Security;

public class JwtTokenGenerator
{
    private readonly string _key;

    public JwtTokenGenerator(string key)
    {
        _key = key;
    }

    public string GenerateToken(int userId, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email)
        };

        var keyBytes = Encoding.UTF8.GetBytes(_key);
        var creds = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
