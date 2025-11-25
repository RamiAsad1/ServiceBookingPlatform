using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceBookingPlatformApi.DTOs.Auth;
using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Repositories.Interfaces;


namespace ServiceBookingPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IMapper _mapper;

        public AuthController(IAuthService auth, IMapper mapper)
        {
            _auth = auth;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _auth.RegisterAsync(dto);
            if (user == null) return Conflict(new { message = "Email already in use." });

            var userDto = _mapper.Map<UserDto>(user);
            return CreatedAtAction(null, userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var token = await _auth.LoginAsync(login.Email, login.Password);
            if (token == null) return Unauthorized();

            return Ok(new { token });
        }
    }

}
