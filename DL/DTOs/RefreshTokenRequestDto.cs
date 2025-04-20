namespace Data_Layer.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for refreshing a token.
    /// </summary>
    public class RefreshTokenRequestDto
    {
        /// <summary>
        /// The unique Id of the user requesting the token refresh.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// The user's refresh token.
        /// </summary>
        public required string RefreshToken { get; set; }

        /// <summary>
        /// The IP Address from which the user request the token refresh.
        /// </summary>
        public required string IpAddress { get; set; }
    }
}
