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
    public class VehicleController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly ILogger<VehicleController> _logger;
        private readonly IMapper _mapper;

        public VehicleController(ModelContext context, ILogger<VehicleController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = await _context.Vehicles
                                  .Where(e => e.IsDeleted == false)
                                  .Include(v => v.Images)
                                  
                                  .AsNoTracking()
                                  .ToListAsync();


                var response = _mapper.Map<List<VehicleViewModel>>(query);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById([FromBody] int id)
        {
            try
            {
                var query = await _context.Vehicles
                                  .Where(e => e.VehicleId == id && e.IsDeleted == false)
                                  .Include(v => v.Images)
                                  .Select(v => new VehicleViewModel
                                  {
                                      Id = v.VehicleId,
                                      Name = v.Name,
                                      Model = v.FkVehicleModelId,
                                      PlatNo = v.PlatNo,
                                      Desc = v.Desc,
                                      Price = v.Price,
                                      Image = v.Images.Select(i => new ImageViewModel
                                      {
                                          Path = i.Path ?? string.Empty,
                                      }).ToList()
                                  })
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();

                var response = _mapper.Map<VehicleViewModel>(query);

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }

        [Authorize]
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] VehicleViewModel vehicleViewModel)
        {
            var jwtCookie = Request.Cookies["jwt_admin"] ?? "";
            if (!JwtHelper.IsTokenValid(jwtCookie))
            {
                return Unauthorized(new { Message = "Token Expired, Please Login" });
            }

            try
            {
                var query = _mapper.Map<Vehicle>(vehicleViewModel);
                _context.Vehicles.Add(query);
                await _context.SaveChangesAsync();

                if (vehicleViewModel.Image != null && vehicleViewModel.Image.Any())
                {
                    foreach (var imagePath in vehicleViewModel.Image)
                    {
                        var image = new ImageRequestViewModel
                        {
                            Path = imagePath.Path,
                            vehicleId = query.VehicleId
                        };

                        var mappedImage = _mapper.Map<Image>(image);
                        _context.Image.Add(mappedImage);
                        await _context.SaveChangesAsync();
                    }
                }
                
                return Ok(new { Message = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }

        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] VehicleViewModel vehicleViewModel)
        {
            var jwtCookie = Request.Cookies["jwt_admin"] ?? "";
            if (!JwtHelper.IsTokenValid(jwtCookie))
            {
                return Unauthorized(new { Message = "Token Expired, Please Login" });
            }
            try
            {
                var recordUpdate = await _context.Vehicles
                                         .Where(e => e.VehicleId == vehicleViewModel.Id && !e.IsDeleted)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync();

                if (recordUpdate != null)
                {
                    _mapper.Map(vehicleViewModel, recordUpdate);
                    _context.Vehicles.Update(recordUpdate);
                }

                var imageUpdate = await _context.Image
                                        .Where(e => e.FkVehicleId == vehicleViewModel.Id)
                                        .AsNoTracking()
                                        .ToListAsync();
                if (imageUpdate.Any())
                {
                    _context.Image.RemoveRange(imageUpdate);

                    if (vehicleViewModel.Image != null && vehicleViewModel.Image.Any())
                    {
                        foreach (var imagePath in vehicleViewModel.Image)
                        {
                            var image = new ImageRequestViewModel
                            {
                                Path = imagePath.Path,
                                vehicleId = vehicleViewModel.Id
                            };

                            var mappedImage = _mapper.Map<Image>(image);
                            _context.Image.Add(mappedImage);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { Message = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
        }


        [Authorize]
        [HttpPost("Deactive")]
        public async Task<IActionResult> Deactive([FromBody]int id)
        {
            var jwtCookie = Request.Cookies["jwt_admin"] ?? "";
            if (!JwtHelper.IsTokenValid(jwtCookie))
            {
                return Unauthorized(new { Message = "Token Expired, Please Login" });
            }
            try
            {
                var query = await _context.Vehicles
                                  .Where(e => e.VehicleId == id && !e.IsDeleted)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();

                if(query == null)
                {
                    return BadRequest(new { Message = "No Vehicle Found" });
                }
                query.IsDeleted = true;
                _context.Vehicles.Update(query);
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
