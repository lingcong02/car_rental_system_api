using AutoMapper;
using car_rental_system_api.Database;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.Helper;
using car_rental_system_api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car_rental_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ModelContext context, IConfiguration config, IMapper mapper, ILogger<AdminController> logger)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("Auth")]
        public IActionResult Auth()
        {
            var jwtCookie = Request.Cookies["jwt_admin"] ?? "";
            if (!JwtHelper.IsTokenValid(jwtCookie))
            {
                return Unauthorized(new { Message = "Token Expired, Please Login" });
            }
            return Ok(new { Message = 200 });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            try
            {
                var query = await _context.Admins
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
                    var token = JwtHelper.GenerateJwtToken(query.AdminId, query.Name, _config);
                    Response.Cookies.Append("jwt_admin", token, new CookieOptions
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
            var token = Request.Cookies["jwt_admin"] ?? "";
            Response.Cookies.Append("jwt_admin", token, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { Message = "200" });
        }

        [HttpPut("Insert")]
        public async Task<IActionResult> Insert([FromBody] AdminViewModel adminViewModel)
        {
            try
            {
                var query = _mapper.Map<Admin>(adminViewModel);
                _context.Admins.Add(query);
                await _context.SaveChangesAsync();
                return Ok(new { Message = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }
    }
}
