using AutoMapper;
using car_rental_system_api.Database;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.Helper;
using car_rental_system_api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace car_rental_system_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public BookingController(ModelContext context, ILogger<UserController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPut("Insert")]
        public async Task<IActionResult> Insert([FromBody] BookingViewModel bookingViewModel)
        {
            try
            {
                var jwtCookie = Request.Cookies["jwt"] ?? "";
                if (!JwtHelper.IsTokenValid(jwtCookie))
                {
                    return Unauthorized(new { Message = "Token Expired, Please Login" });
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var userId = tokenHandler.ReadJwtToken(jwtCookie).Subject;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "Something Error" });
                }
                bookingViewModel.UserId = Convert.ToInt32(userId);
                bookingViewModel.BookingNo = GeneralHelper.GenerateBookingNo();
                var query = _mapper.Map<Booking>(bookingViewModel);
                _context.Bookings.Add(query);
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
