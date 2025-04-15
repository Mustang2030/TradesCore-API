namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a user in the e-commerce system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The role of the user (e.g., Admin, Customer).
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Navigation Property to the Review table.
        /// </summary>
        public List<Review>? Reviews { get; set; }

        /// <summary>
        /// Navigation Property to the Order table.
        /// </summary>
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Navigation Property to the Cart table.
        /// </summary>
        public Cart Cart { get; set; }

        /// <summary>
        /// The refresh token for the user.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// The date and time when the refresh token will expire.
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
