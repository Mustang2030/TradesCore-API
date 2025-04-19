using Data_Layer.DTOs;
using Data_Layer.Models;
using Data_Layer.Utilities;

namespace Repository_Layer.IRepositories
{
    /// <summary>
    /// Interface for authentication and authorization services.
    /// </summary>
    public interface IAuthRepo
    {
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="user">
        /// The new user to be registered.
        /// </param>
        /// <param name="password">
        /// The password for the new user.
        /// </param>
        /// <returns>
        /// The result of the registration operation, indicating success or failure.
        /// </returns>
        Task<OperationResult<TradesCoreUser>> RegisterAsync(TradesCoreUser user, string password);

        /// <summary>
        /// Logs in a user to the system.
        /// </summary>
        /// <param name="userInfo">
        /// The user information containing the username and password for login.
        /// </param>
        /// <returns>
        /// The result of the login operation, including the access and refresh tokens if successful.
        /// </returns>
        Task<OperationResult<TokenResponseDto>> LoginAsync(SignInDto userInfo);

        /// <summary>
        /// Refreshes the access and refresh tokens for a user.
        /// </summary>
        /// <param name="request">
        /// Request containing the refresh token to be used for generating new tokens.
        /// </param>
        /// <returns>
        /// The result of the token refresh operation, including the new access and refresh tokens if successful.
        /// </returns>
        Task<OperationResult<TokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
