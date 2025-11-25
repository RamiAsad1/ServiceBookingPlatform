namespace ServiceBookingPlatformApi.Repositories.Implementations
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ServiceBookingPlatformApi.DTOs.Users;
    using ServiceBookingPlatformApi.Entities.Users;
    using ServiceBookingPlatformApi.Repositories.Interfaces;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class AuthService : IAuthService
    {
        private readonly UnitOfWork _uow;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _config;

        public AuthService(UnitOfWork uow, IPasswordHasher<User> passwordHasher, IConfiguration config)
        {
            _uow = uow;
            _passwordHasher = passwordHasher;
            _config = config;
        }

        public async Task<User?> RegisterAsync(CreateUserDto dto)
        {
            var existing = (await _uow.Repository<User>().GetAllAsync()).FirstOrDefault(u => u.Email == dto.Email);
            if (existing != null) return null;

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                RoleId = dto.RoleId 
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _uow.Repository<User>().AddAsync(user);
            await _uow.SaveAsync();
            return user;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = (await _uow.Repository<User>().GetAllAsync()).FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash ?? "", password);
            if (result == PasswordVerificationResult.Failed) return null;

            var token = GenerateJwtToken(user);
            return token;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSection = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"] ?? throw new InvalidOperationException("JWT key missing")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

            if (user.Role != null && !string.IsNullOrEmpty(user.Role.Name))
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
            else if (user.RoleId.HasValue)
                claims.Add(new Claim(ClaimTypes.Role, user.RoleId.Value.ToString()));

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSection["ExpireMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
