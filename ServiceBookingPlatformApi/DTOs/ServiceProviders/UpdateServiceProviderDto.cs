using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.DTOs.ServiceProviders
{
    public class UpdateServiceProviderDto
    {
        public int? UserId { get; set; }
        public string? BusinessName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
