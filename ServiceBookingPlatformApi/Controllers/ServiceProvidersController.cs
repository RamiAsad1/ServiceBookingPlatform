using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.DTOs.ServiceProviders;
using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Entities.Users;
using ServiceBookingPlatformApi.Repositories;

namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceProvidersController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllServiceProviders()
        {
            var serviceProviders = await _unitOfWork.Repository<Entities.Users.ServiceProvider>().GetAllAsync();
            var serviceProvidersDto = _mapper.Map<IEnumerable<ServiceProviderDto>>(serviceProviders);
            return Ok(serviceProvidersDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceProviderById(int id)
        {
            var serviceProvider = await _unitOfWork.Repository<Entities.Users.ServiceProvider>().GetByIdAsync(id);
            if (serviceProvider == null)
            {
                return NotFound();
            }
            var serviceProviderDto = _mapper.Map<ServiceProviderDto>(serviceProvider);
            return Ok(serviceProviderDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateServiceProvider([FromBody] CreateServiceProviderDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var serviceProvider = _mapper.Map<Entities.Users.ServiceProvider>(createDto);
            await _unitOfWork.Repository<Entities.Users.ServiceProvider>().AddAsync(serviceProvider);
            await _unitOfWork.SaveAsync();
            var serviceProviderDto = _mapper.Map<ServiceProviderDto>(serviceProvider);

            return CreatedAtAction(nameof(GetServiceProviderById), new { id = serviceProvider.Id }, serviceProviderDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceProvider(int id, [FromBody] UpdateServiceProviderDto updateDto)
        {
            var existingServiceProvider = await _unitOfWork.Repository<Entities.Users.ServiceProvider>().GetByIdAsync(id);
            if (existingServiceProvider == null)
                return NotFound();

            _mapper.Map(updateDto, existingServiceProvider);

            _unitOfWork.Repository<Entities.Users.ServiceProvider>().Update(existingServiceProvider);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
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
