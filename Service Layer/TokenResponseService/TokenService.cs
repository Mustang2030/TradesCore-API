using Data_Layer.DTOs;

namespace Service_Layer.TokenResponseService
{
    public class TokenService
    {
        public static TokenResponseDto CleanTokenResponse(TokenResponseDto tokenResponseDto)
        {
            tokenResponseDto.RefreshToken.User = null;
            return tokenResponseDto;
        }
    }
}
