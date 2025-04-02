using AutoMapper;
using car_rental_system_api.Database;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.ViewModel;
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
                                  .ToListAsync();


                var response = _mapper.Map<List<VehicleViewModel>>(query);

                return Ok(query);
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

        [HttpPut("Insert")]
        public async Task<IActionResult> Insert([FromBody] VehicleViewModel vehicleViewModel)
        {
            try
            {
                var query = _mapper.Map<Vehicle>(vehicleViewModel);
                _context.Vehicles.Add(query);
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
