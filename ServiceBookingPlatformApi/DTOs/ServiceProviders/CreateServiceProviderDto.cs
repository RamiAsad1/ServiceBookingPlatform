using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.DTOs.ServiceProviders
{
    public class CreateServiceProviderDto
    {
        public int UserId { get; set; }
        public required string BusinessName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
