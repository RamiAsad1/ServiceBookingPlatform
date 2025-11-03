using Microsoft.EntityFrameworkCore;
using ServiceBookingPlatformApi.Entities.Bookings;
using ServiceBookingPlatformApi.Entities.Services;
using ServiceBookingPlatformApi.Entities.Users;

namespace ServiceBookingPlatformApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Entities.Users.ServiceProvider> ServiceProviderProfiles => Set<Entities.Users.ServiceProvider>();
        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<Service> Services => Set<Service>();   
        public DbSet<ServiceImage> ServiceImages => Set<ServiceImage>();
        public DbSet<Booking> Bookings  => Set<Booking>();  
        public DbSet<BookingStatus> BookingStatus => Set<BookingStatus>();
        public DbSet<Review> Reviews => Set<Review>();
  

    }
}
