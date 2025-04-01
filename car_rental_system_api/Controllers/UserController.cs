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
        private readonly IMapper _mapper;

        public UserController(ModelContext context, ILogger<UserController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
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
            var jwtCookie = Request.Cookies["jwt"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var userId = tokenHandler.ReadJwtToken(jwtCookie).Subject;

            if (string.IsNullOrEmpty(jwtCookie))
            {
                return Unauthorized("JWT Cookie is missing");
            }
            if (!Request.Cookies.TryGetValue("jwt", out var token))
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
    }
}
