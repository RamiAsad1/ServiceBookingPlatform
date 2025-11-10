using ServiceBookingPlatformApi.Entities.Bookings;

namespace ServiceBookingPlatformApi.DTOs.Reviews
{
    public class UpdateReviewDto
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
