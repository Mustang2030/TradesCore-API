using Data_Layer.DTOs;
using Data_Layer.Models;

namespace TradesCore_API.IServices
{
    /// <summary>
    /// Interface for authentication and authorization services.
    /// </summary>
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
