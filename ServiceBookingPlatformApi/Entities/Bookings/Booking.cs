namespace ServiceBookingPlatformApi.Entities.Bookings
{
    public class Booking
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
        public int ReviewId { get; set; }
        public string? Status { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public Users.ServiceProvider? ServiceProvider { get; set; }
        public Services.Service? Service { get; set; }
        public Users.User? User { get; set; }
        public ICollection<BookingStatus> StatusHistory { get; set; } = new List<BookingStatus>();
        public Review? Review { get; set; }

    }
}
