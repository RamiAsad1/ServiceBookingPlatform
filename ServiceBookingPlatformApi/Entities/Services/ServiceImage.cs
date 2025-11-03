namespace ServiceBookingPlatformApi.Entities.Services
{
    public class ServiceImage
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string? ImageUrl { get; set; }

        public Service? Service { get; set; }
    }
}
