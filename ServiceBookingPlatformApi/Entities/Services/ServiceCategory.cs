namespace ServiceBookingPlatformApi.Entities.Services
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();

    }
}
