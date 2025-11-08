namespace ServiceBookingPlatformApi.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public String? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ServiceProvider? ServiceProvider { get; set; }
        public Role? Role { get; set; }

    }
}
