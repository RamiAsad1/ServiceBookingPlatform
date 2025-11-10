using ServiceBookingPlatformApi.Entities.Bookings;

namespace ServiceBookingPlatformApi.DTOs.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
        public int ReviewId { get; set; }
        public required string Status { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<BookingStatus> StatusHistory { get; set; } = new List<BookingStatus>();
    }
}
