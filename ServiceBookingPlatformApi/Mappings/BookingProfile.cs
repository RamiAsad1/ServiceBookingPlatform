using AutoMapper;
using ServiceBookingPlatformApi.DTOs.Bookings;
using ServiceBookingPlatformApi.Entities.Bookings;

namespace ServiceBookingPlatformApi.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile() {
            CreateMap<Booking, BookingDto>();
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<UpdateBookingDto, Booking>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
