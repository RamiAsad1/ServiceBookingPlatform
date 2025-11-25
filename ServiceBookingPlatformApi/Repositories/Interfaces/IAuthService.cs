using ServiceBookingPlatformApi.DTOs.Users;
using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.Repositories.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(CreateUserDto dto);
        Task<string?> LoginAsync(string email, string password);
    }

}
