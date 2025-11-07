using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.Entities.Services;
using ServiceBookingPlatformApi.Repositories;

namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        public readonly UnitOfWork _unitOfWork;

        public ServicesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var Services = await _unitOfWork.Repository<Service>().GetAllAsync();
            return Ok(Services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var Service = await _unitOfWork.Repository<Service>().GetByIdAsync(id);
            if (Service == null)
            {
                return NotFound();
            }
            return Ok(Service);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] Service Service)
        {
            await _unitOfWork.Repository<Service>().AddAsync(Service);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetServiceById), new { id = Service.Id }, Service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] Service Service)
        {
            if (id != Service.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Repository<Service>().Update(Service);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var Service = await _unitOfWork.Repository<Service>().GetByIdAsync(id);
            if (Service == null)
            {
                return NotFound();
            }
            _unitOfWork.Repository<Service>().Delete(Service);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
