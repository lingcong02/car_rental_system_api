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
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "user"),
                new Claim("uid", userId.ToString()),
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

        public static bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            var jwtHandler = new JwtSecurityTokenHandler();

            // Parse the JWT token
            var jsonToken = jwtHandler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
                throw new Exception("Invalid token format.");

            // Get the expiration claim
            var expClaim = jsonToken?.Claims?.FirstOrDefault(c => c.Type == "exp");

            if (expClaim == null)
                throw new Exception("Token does not have an expiration claim.");

            // Convert the expiration to a DateTime
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value)).DateTime;

            // Check if the token is expired
            return expirationDate > DateTime.UtcNow;
        }
    }
}
