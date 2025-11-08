namespace ServiceBookingPlatformApi.Entities.Services
{
    public class Service
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ServiceProviderId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan? Duration { get; set; }
        public ServiceCategory? Category { get; set; }
        public Users.ServiceProvider? ServiceProvider { get; set; }
        public ICollection<ServiceImage> Images { get; set; } = new List<ServiceImage>();

    }
}
