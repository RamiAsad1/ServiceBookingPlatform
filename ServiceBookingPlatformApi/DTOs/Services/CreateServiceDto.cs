using ServiceBookingPlatformApi.Entities.Services;

namespace ServiceBookingPlatformApi.DTOs.Services
{
    public class CreateServiceDto
    {
        public int CategoryId { get; set; }
        public int ServiceProviderId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
