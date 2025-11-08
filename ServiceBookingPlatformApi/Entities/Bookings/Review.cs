namespace ServiceBookingPlatformApi.Entities.Bookings
{
    public class Review
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Users.User? User { get; set; }   
        public Booking? Booking { get; set; }
    }
}
