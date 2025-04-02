using AutoMapper;
using car_rental_system_api.Database;
using car_rental_system_api.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car_rental_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ModelContext context, IConfiguration config, IMapper mapper, ILogger<AuthController> logger)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var query = await _context.Users
                                  .Where(e => e.Email == request.Email)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();

                if (query == null)
                {
                    return Unauthorized(new { Message = "Invalid Email or Password" });
                }

                var verifyPassword = CryptoHelper.VerifyPassword(request.Password, query.Hash, query.Guid);

                if (verifyPassword)
                {
                    var token = JwtHelper.GenerateJwtToken(query.UserId, query.Name, _config);
                    Response.Cookies.Append("jwt", token, new CookieOptions
                    {
                        HttpOnly = true,  // Prevents JavaScript access
                        Secure = true,    // Requires HTTPS
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMinutes(60)
                    });
                    return Ok(new { Message = 200 });
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid Password" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            var token = Request.Cookies["jwt"] ?? "";
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { Message = "200" });
        }

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
