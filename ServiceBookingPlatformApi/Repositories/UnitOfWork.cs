using ServiceBookingPlatformApi.Data;
using ServiceBookingPlatformApi.Entities.Users;
using ServiceBookingPlatformApi.Repositories.Implementations;
using ServiceBookingPlatformApi.Repositories.Interfaces;

namespace ServiceBookingPlatformApi.Repositories
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        //public UserRepository Users { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            //Users = new UserRepository(context);
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }

}
