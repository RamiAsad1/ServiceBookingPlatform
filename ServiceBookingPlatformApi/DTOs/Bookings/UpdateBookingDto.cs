using ServiceBookingPlatformApi.Entities.Bookings;

namespace ServiceBookingPlatformApi.DTOs.Bookings
{
    public class UpdateBookingDto
    {
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public int ProviderId { get; set; }
        public int ReviewId { get; set; }
        public string? Status { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
