namespace Data_Layer.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } 
        public List<Review>? Reviews { get; set; }
        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
