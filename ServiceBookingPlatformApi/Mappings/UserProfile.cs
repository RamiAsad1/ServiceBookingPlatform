using AutoMapper;
using ServiceBookingPlatformApi.Domain.Enums;
using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserDto>().ForMember(dest => dest.RoleName,opt => opt.MapFrom(src => src.Role!.ToString()));
            CreateMap<CreateUserDto, User>().ForMember(dest => dest.Role,opt => opt.MapFrom(src => Enum.Parse<RoleType>(src.RoleName!)));
            CreateMap<UpdateUserDto,User>().ForMember(dest => dest.Role, opt =>opt.MapFrom(src => Enum.Parse<RoleType>(src.RoleName!))).
                ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
