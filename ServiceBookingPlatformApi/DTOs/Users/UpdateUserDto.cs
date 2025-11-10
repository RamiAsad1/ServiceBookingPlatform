using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.DTOs.Users
{
    public class UpdateUserDto
    {
        public int? RoleId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
