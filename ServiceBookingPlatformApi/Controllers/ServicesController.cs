using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.DTOs.Services;
using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Entities.Services;
using ServiceBookingPlatformApi.Entities.Users;
using ServiceBookingPlatformApi.Repositories;

namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicesController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _unitOfWork.Repository<Service>().GetAllAsync();
            var servicesDto = _mapper.Map<IEnumerable<ServiceDto>>(services);

            return Ok(servicesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _unitOfWork.Repository<Service>().GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            var serviceDto = _mapper.Map<ServiceDto>(service);

            return Ok(serviceDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = _mapper.Map<Service>(createDto);
            await _unitOfWork.Repository<Service>().AddAsync(service);
            await _unitOfWork.SaveAsync();
            var serviceDto = _mapper.Map<ServiceDto>(service);

            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, serviceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceDto updateDto)
        {
            var existingService = await _unitOfWork.Repository<Service>().GetByIdAsync(id);
            if (existingService == null)
                return NotFound();

            _mapper.Map(updateDto, existingService);

            _unitOfWork.Repository<Service>().Update(existingService);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _unitOfWork.Repository<Service>().GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            _unitOfWork.Repository<Service>().Delete(service);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
