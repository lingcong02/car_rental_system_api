using AutoMapper;
using car_rental_system_api.Database;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.Helper;
using car_rental_system_api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace car_rental_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;        

        public UserController(ModelContext context, ILogger<UserController> logger, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById([FromBody] int id)
        {
            try
            {
                var query = await _context.Users
                                  .Where(e => e.UserId == id && e.IsDeleted == false)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();
                var response = _mapper.Map<UserViewModel>(query);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }

        [Authorize]
        [HttpGet("GetByJwt")]
        public async Task<IActionResult> GetByJwt()
        {
            var jwtCookie = Request.Cookies["jwt_user"] ?? "";
            if (!JwtHelper.IsTokenValid(jwtCookie))
            {
                return Unauthorized(new { Message = "Token Expired, Please Login" });
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var userId = tokenHandler.ReadJwtToken(jwtCookie).Subject;

            try
            {
                var query = await _context.Users
                                  .Where(e => e.UserId == Convert.ToInt32(userId) && e.IsDeleted == false)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();
                var response = _mapper.Map<UserViewModel>(query);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }

        [HttpPut("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserViewModel userViewModel)
        {
            try
            {
                var query = _mapper.Map<User>(userViewModel);
                _context.Users.Add(query);
                await _context.SaveChangesAsync();
                return Ok(new { Message = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }

        [Authorize]
        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromBody] UserViewModel userViewModel)
        {
            var jwtCookie = Request.Cookies["jwt_user"] ?? "";
            if (!JwtHelper.IsTokenValid(jwtCookie))
            {
                return Unauthorized(new { Message = "Token Expired, Please Login" });
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var userId = tokenHandler.ReadJwtToken(jwtCookie).Subject;

            if (string.IsNullOrEmpty(jwtCookie))
            {
                return Unauthorized(new { Message = "JWT Cookie is missing" });
            }
            if (!Request.Cookies.TryGetValue("jwt_user", out var token))
            {
                return Unauthorized(new { Message = "No Token Found, Please Login" });
            }
            //var userId = JwtHelper.GetUserIdFromToken(HttpContext);


            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User ID not found" });
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User ID Not Found" });
            }
            if (userId != null)
            {
                return Ok(userId);
            }
            return Ok(userId);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
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
                    Response.Cookies.Append("jwt_user", token, new CookieOptions
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
            var token = Request.Cookies["jwt_user"] ?? "";
            Response.Cookies.Append("jwt_user", token, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { Message = "200" });
        }
    }
}
