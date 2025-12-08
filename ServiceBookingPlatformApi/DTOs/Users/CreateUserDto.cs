using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.DTOs.Users
{
    public class CreateUserDto
    {
        public string? Username { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? RoleName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
