using AutoMapper;
using ServiceBookingPlatformApi.DTOs.ServiceProviders;

namespace ServiceBookingPlatformApi.Mappings
{
    public class ServiceProviderProfile : Profile
    {
        public ServiceProviderProfile() {
            CreateMap<ServiceProvider, ServiceProviderDto>();
            CreateMap<CreateServiceProviderDto, ServiceProvider>();
            CreateMap<UpdateServiceProviderDto, ServiceProvider>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
