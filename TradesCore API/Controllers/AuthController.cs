using AutoMapper;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;
using Service_Layer.TokenResponseService;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepo authService, IMapper mapper) : ControllerBase
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
                var result = await authService.RegisterAsync(mapper.Map<TradesCoreUser>(request), password);
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
                var result = await authService.LoginAsync(data);
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
                var result = await authService.RefreshTokenAsync(request);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                HttpContext.Response.Cookies.Append(accessToken, result.Data!.AccessToken, cookieOptions);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("registerd-users-only")]
        public IActionResult AuthenticatedOnlyEndPoit()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are an Admin");
        }
    }
}
