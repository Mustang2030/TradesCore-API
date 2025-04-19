using Data_Layer.Models;

namespace Data_Layer.DTOs
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}
