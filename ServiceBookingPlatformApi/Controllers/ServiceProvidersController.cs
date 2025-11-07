using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.Repositories;

namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ServiceProvidersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllServiceProviders()
        {
            var ServiceProviders = await _unitOfWork.Repository<Entities.Users.ServiceProvider>().GetAllAsync();
            return Ok(ServiceProviders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceProviderById(int id)
        {
            var ServiceProvider = await _unitOfWork.Repository<Entities.Users.ServiceProvider>().GetByIdAsync(id);
            if (ServiceProvider == null)
            {
                return NotFound();
            }
            return Ok(ServiceProvider);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceProvider([FromBody] Entities.Users.ServiceProvider ServiceProvider)
        {
            await _unitOfWork.Repository<Entities.Users.ServiceProvider>().AddAsync(ServiceProvider);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetServiceProviderById), new { id = ServiceProvider.Id}, ServiceProvider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceProvider(int id, [FromBody] Entities.Users.ServiceProvider ServiceProvider)
        {
            if (id != ServiceProvider.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Repository<Entities.Users.ServiceProvider>().Update(ServiceProvider);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceProvider(int id)
        {
            var ServiceProvider = await _unitOfWork.Repository<Entities.Users.ServiceProvider>().GetByIdAsync(id);
            if (ServiceProvider == null)
            {
                return NotFound();
            }
            _unitOfWork.Repository<Entities.Users.ServiceProvider>().Delete(ServiceProvider);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
