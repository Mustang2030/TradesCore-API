using Microsoft.AspNetCore.Authorization;
using Repository_Layer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Data_Layer.Models;
using Data_Layer.DTOs;
using AutoMapper;
using System.Linq.Expressions;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepo authRepo, IMapper mapper) : ControllerBase
    {
        private const string accessToken = "access_token";
        
        private readonly CookieOptions cookieOptions = new()
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request, string password) 
        {
            try
            {
                var result = await authRepo.RegisterAsync(mapper.Map<TradesCoreUser>(request), password);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(SignInDto data)
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

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string value)
        {
            try
            {
                var result = await authRepo.ConfirmEmailAsync(userId, value);
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
