using AutoMapper;
using ServiceBookingPlatformApi.DTOs.Services;
using ServiceBookingPlatformApi.Entities.Services;

namespace ServiceBookingPlatformApi.Mappings
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() { 
            CreateMap<Service,ServiceDto>();
            CreateMap<CreateServiceDto,Service>();
            CreateMap<UpdateServiceDto,Service>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
