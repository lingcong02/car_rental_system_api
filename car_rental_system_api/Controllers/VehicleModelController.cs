using AutoMapper;
using car_rental_system_api.Database;
using car_rental_system_api.Database.Entity;
using car_rental_system_api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car_rental_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly ILogger<VehicleModelController> _logger;
        private readonly IMapper _mapper;

        public VehicleModelController(ModelContext context, ILogger<VehicleModelController> logger, IMapper mapper)
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
                var query = await _context.VehicleModels
                                  .Where(e => e.IsDeleted == false)
                                  .AsNoTracking()
                                  .ToListAsync();
                var response = _mapper.Map<List<VehicleModelViewModel>>(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById([FromBody] int id)
        {
            try
            {
                var query = await _context.VehicleModels
                                  .Where(e => e.VehicleModelId == id && e.IsDeleted == false)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();
                var response = _mapper.Map<VehicleModelViewModel>(query);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("Insert")]
        public async Task<IActionResult> Insert([FromBody] VehicleModelViewModel vehicleViewModel)
        {
            try
            {
                var query = _mapper.Map<VehicleModel>(vehicleViewModel);
                _context.VehicleModels.Add(query);
                await _context.SaveChangesAsync();
                return Ok(new { Message = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update([FromBody] VehicleModelViewModel vehicleViewModel)
        {
            try
            {
                var recordUpdate = await _context.VehicleModels
                                         .Where(e => e.VehicleModelId == vehicleViewModel.Id && e.IsDeleted == false)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync();
                if (recordUpdate != null)
                {
                    _mapper.Map(vehicleViewModel, recordUpdate);
                    _context.VehicleModels.Update(recordUpdate);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = 200 });
                }
                else
                {
                    return BadRequest("Vehicle Model Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                var recordDelete = await _context.VehicleModels
                                         .Where(e => e.VehicleModelId == id && e.IsDeleted == false)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync();

                if (recordDelete != null)
                {
                    recordDelete.IsDeleted = true;
                    _context.VehicleModels.Update(recordDelete);
                    var response = await _context.SaveChangesAsync();
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Vehicle Model Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
