using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace car_rental_system_api.Helper
{
    public class JwtHelper
    {
        public static string GenerateJwtToken(int userId, string username, IConfiguration config)
        {
            var key = Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]!);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(config["JwtSettings:ExpiryMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = config["JwtSettings:Issuer"],
                Audience = config["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string? GetUserIdFromToken(HttpContext httpContext)
        {
            return httpContext.User.FindFirst("UserId")?.Value; // ✅ Extract UserId from claims
        }
    }
}
