namespace ServiceBookingPlatformApi.Entities.Users
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String? BusinessName { get; set; }
        public String? Description { get; set; }
        public String? Address { get; set; }
        public String? PhoneNumber { get; set; }

        public User? User { get; set; }
    }
}
