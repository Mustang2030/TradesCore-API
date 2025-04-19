namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a refresh token used for user authentication.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Unique identifier for the refresh token.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The token string.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The date and time when the token was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// The date and time when the token expires.
        /// </summary>
        public DateTime ExpiresAt { get; private set; } = DateTime.UtcNow.AddDays(7);

        /// <summary>
        /// The IP address from which the token was created.
        /// </summary>
        public string CreatedByIp { get; set; }

        /// <summary>
        /// Foreign key to the User table.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property to the User table.
        /// </summary>
        public TradesCoreUser? User { get; set; }
    }
}
