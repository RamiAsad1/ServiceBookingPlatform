using AutoMapper;
using ServiceBookingPlatformApi.DTOs.Reviews;
using ServiceBookingPlatformApi.Entities.Bookings;

namespace ServiceBookingPlatformApi.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile() {
            CreateMap<Review,ReviewDto>();
            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
