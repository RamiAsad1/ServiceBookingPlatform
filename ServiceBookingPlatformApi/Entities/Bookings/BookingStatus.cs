namespace ServiceBookingPlatformApi.Entities.Bookings
{
    public class BookingStatus
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string? Status { get; set; }
        public DateTime ChangedAt { get; set; }
        public Booking? Booking { get; set; }
    }
}
