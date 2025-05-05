using Repository_Layer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Data_Layer.Models;
using Data_Layer.DTOs;
using AutoMapper;

namespace TradesCore_API.Controllers
{
    /// <summary>
    /// API controller that handles authentication and authorization for the application.
    /// </summary>
    /// <param name="authRepo">
    /// The repository that handles authentication and authorization operations.
    /// </param>
    /// <param name="mapper">
    /// The AutoMapper instance used for mapping between DTOs and domain models.
    /// </param>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepo authRepo, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// The name of the cookie used to store the access token.
        /// </summary>
        private const string accessToken = "access_token";

        /// <summary>
        /// The options for the cookie used to store the access token.
        /// </summary>
        private readonly CookieOptions cookieOptions = new()
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        /// <summary>
        /// API endpoint for user registration.
        /// </summary>
        /// <param name="user">
        /// The user information to be registered.
        /// </param>
        /// <param name="password">
        /// The password for the new user.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the registration operation.
        /// </returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user, string password) 
        {
            try
            {
                var result = await authRepo.RegisterAsync(mapper.Map<TradesCoreUser>(user), password);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for user login.
        /// </summary>
        /// <param name="data">
        /// The user information containing the username and password for login.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the access and refresh tokens if successful.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInDto data)
        {
            try
            {
                var result = await authRepo.LoginAsync(data);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                HttpContext.Response.Cookies.Append(accessToken, result.Data!.AccessToken, cookieOptions);

                return Ok(result.Data.RefreshToken!.Token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for refreshing the access token.
        /// </summary>
        /// <param name="request">
        /// The request containing the refresh token to be used for generating new tokens.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the token refresh operation.
        /// </returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            try
            {
                var result = await authRepo.RefreshTokenAsync(request);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                HttpContext.Response.Cookies.Append(accessToken, result.Data!.AccessToken, cookieOptions);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for confirming the user's email address.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose email address is being verified.
        /// </param>
        /// <param name="token">
        /// The verification token with which to verify the email address.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the email confirmation operation.
        /// </returns>
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            try
            {
                var result = await authRepo.ConfirmEmailAsync(userId, token);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
