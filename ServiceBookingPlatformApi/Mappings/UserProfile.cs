using AutoMapper;
using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User,UserDto>();
            CreateMap<CreateUserDto,User>();
            CreateMap<UpdateUserDto,User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
