namespace Data_Layer.DTOs
{
    public class RefreshTokenRequestDto
    {
        public required string UserId { get; set; }
        public required string RefreshToken { get; set; }
        public required string IpAddress { get; set; }
    }
}
