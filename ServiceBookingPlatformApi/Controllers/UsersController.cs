using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Entities.Users;
using ServiceBookingPlatformApi.Repositories;

namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = _mapper.Map<User>(createDto);
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveAsync();
            var userDto = _mapper.Map<UserDto>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateDto)
        {
            var existingUser = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            _mapper.Map(updateDto, existingUser);

            _unitOfWork.Repository<User>().Update(existingUser);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _unitOfWork.Repository<User>().Delete(user);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
