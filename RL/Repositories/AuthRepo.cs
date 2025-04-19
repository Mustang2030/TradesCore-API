using Data_Layer.Data;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.IRepositories;
using System.Security.Cryptography;
using System.Security.Claims;
using Data_Layer.Utilities;
using System.Text;

namespace Repository_Layer.Repositories
{
    /// <summary>
    /// Class for handling authentication and authorization operations.
    /// </summary>
    /// <param name="context">
    /// The database context used for data access.
    /// </param>
    /// <param name="signInManager">
    /// The SignInManager used for user sign-in and registration operations.
    /// </param>
    /// <param name="configuration">
    /// The configuration object used for accessing application settings.
    /// </param>
    public class AuthRepo(TradesCoreDbContext context, SignInManager<TradesCoreUser> signInManager, IConfiguration configuration) : IAuthRepo
    {
        /// <summary>
        /// Token creation failure message.
        /// </summary>
        private const string tokenCreationFailure = "Token Creation Failed!";

        /// <summary>
        /// Method to check if a username already exists in the database.
        /// </summary>
        /// <param name="username">
        /// Username to check for existence.
        /// </param>
        /// <returns>
        /// A <see cref="bool"/> indicating whether the username exists.
        /// </returns>
        private bool UsernameExists(string? username)
        {
            return username is not null && signInManager.UserManager.FindByNameAsync(username).Result is not null;
        }

        /// <summary>
        /// Method to check if a phone number already exists in the database.
        /// </summary>
        /// <param name="phoneNumber">
        /// The phone number to check for existence.
        /// </param>
        /// <returns>
        /// A <see cref="bool"/> indicating whether the phone number exists.
        /// </returns>
        private bool PhoneNumberExists(string? phoneNumber)
        {
            return phoneNumber is not null && context.Users.Any(u => u.PhoneNumber == phoneNumber);
        }

        /// <summary>
        /// Method to process the results <see cref="IdentityResult"/> operation.
        /// </summary>
        /// <param name="result">
        /// The <see cref="IdentityResult"/> to process.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{T}"/> containing the result of the operation.
        /// </returns>
        public static OperationResult<IdentityResult> IdResult(IdentityResult result)
        {
            if (result.Succeeded)
            {
                return OperationResult<IdentityResult>.SuccessResult();
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(err => err.Description));
                return OperationResult<IdentityResult>.Failure($"Failed to create user: {errors}");
            }
        }

        /// <summary>
        /// Method to register a new user in the system.
        /// </summary>
        /// <param name="newUser">
        /// The new user to register.
        /// </param>
        /// <param name="password">
        /// The password for the new user.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{T}"/> containing the result of the operation.
        /// </returns>
        public async Task<OperationResult<TradesCoreUser>> RegisterAsync(TradesCoreUser newUser, string password)
        {
            try
            {
                var user = await signInManager.UserManager.FindByEmailAsync(newUser.Email!);
                if (user is not null) throw new($"User with the email {newUser.Email} already exists.");

                if (PhoneNumberExists(newUser.PhoneNumber))
                    throw new($"User with the phone number {newUser.PhoneNumber} already exists.");

                var result = IdResult(await signInManager.UserManager.CreateAsync(newUser, password));
                if (!result.Success) throw new(result.ErrorMessage);

                result = IdResult(await signInManager.UserManager.AddToRoleAsync(newUser, newUser.Role));
                if (!result.Success) throw new(result.ErrorMessage);

                result = IdResult(await signInManager.UserManager.AddClaimsAsync(newUser,
                [
                    new Claim("username", newUser.UserName!),
                    new Claim("id", newUser.Id),
                    new Claim(ClaimTypes.Email, newUser.Email!),
                    new Claim(ClaimTypes.MobilePhone, newUser.PhoneNumber!),
                    new Claim(ClaimTypes.Role, newUser.Role)
                ]));
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Method to log in a user to the system.
        /// </summary>
        /// <param name="userInfo">
        /// The user information containing the username and password for login.
        /// </param>
        /// <returns>
        /// The result of the login operation, including the access and refresh tokens if successful.
        /// </returns>
        public async Task<OperationResult<TokenResponseDto>> LoginAsync(SignInDto userInfo)
        {
            try
            {
                const string invalidCreds = "Invalid credentials.";
                var user = await signInManager.UserManager.FindByEmailAsync(userInfo.Email);
                if (user is null) throw new(invalidCreds);

                var result = await signInManager.CheckPasswordSignInAsync(user, userInfo.Password, false);
                if (!result.Succeeded) throw new(invalidCreds);

                var opResult = await CreateTokensAsync(user, userInfo.SignedInFromIp);
                if (!opResult.Success) throw new(opResult.ErrorMessage);

                return OperationResult<TokenResponseDto>.SuccessResult(opResult.Data!);
            }
            catch (Exception ex)
            {
                return OperationResult<TokenResponseDto>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Method to create access and refresh tokens for a user.
        /// </summary>
        /// <param name="user">
        /// The user for whom to create the tokens.
        /// </param>
        /// <param name="ipAdd">
        /// The IP address from which the user signed in.
        /// </param>
        /// <returns>
        /// The result of the token creation operation, including the access and refresh tokens if successful.
        /// </returns>
        private async Task<OperationResult<TokenResponseDto>> CreateTokensAsync(TradesCoreUser user, string ipAdd)
        {
            try
            {
                var claims = await signInManager.UserManager.GetClaimsAsync(user);

                var result = await GenerateAndSaveRefreshTokenAsync(user, ipAdd);
                if (!result.Success) throw new(result.ErrorMessage);

                var tkResponse = new TokenResponseDto
                {
                    AccessToken = CreateToken(claims),
                    RefreshToken = result.Data!
                };

                return OperationResult<TokenResponseDto>.SuccessResult(tkResponse);
            }
            catch (Exception ex)
            {
                return OperationResult<TokenResponseDto>.Failure($"{ex.Message}\nInner Exception: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to refresh the access and refresh tokens for a user.
        /// </summary>
        /// <param name="request">
        /// The request containing the refresh token and user ID.
        /// </param>
        /// <returns>
        /// The result of the refresh token operation, including the new access and refresh tokens if successful.
        /// </returns>
        public async Task<OperationResult<TokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            try
            {
                var result = await ValidateRefreshTokenAsync(request);
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TokenResponseDto>.SuccessResult(new()
                {
                    AccessToken = CreateToken(await signInManager.UserManager.GetClaimsAsync(result.Data!)),
                });
            }
            catch (Exception ex)
            {
                return OperationResult<TokenResponseDto>.Failure($"{ex.Message}\nInner Exception: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to validate a refresh token.
        /// </summary>
        /// <param name="refreshTokenRequest">
        /// The request containing the refresh token and user ID.
        /// </param>
        /// <returns>
        /// The result of the validation operation.
        /// </returns>
        private async Task<OperationResult<TradesCoreUser>> ValidateRefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest)
        {
            try
            {
                var user = await context.Users.Include(r => r.RefreshToken).FirstOrDefaultAsync(u => u.Id == refreshTokenRequest.UserId);
                if (user is null) throw new("User with the specified ID not found.");

                if (user.RefreshToken.Token != refreshTokenRequest.RefreshToken || user.RefreshToken.ExpiresAt <= DateTime.UtcNow)
                    throw new("Invalid refresh token.");

                return OperationResult<TradesCoreUser>.SuccessResult(user);
            }
            catch (Exception ex)
            {
                return OperationResult<TradesCoreUser>.Failure($"{ex.Message}\nInner Exception: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to generate a refresh token.
        /// </summary>
        /// <returns>
        /// Newly generated refresh token as a string.
        /// </returns>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Method to generate and persist a refresh token for a user.
        /// </summary>
        /// <param name="user">
        /// The user for whom the token is created.
        /// </param>
        /// <param name="ipAdd">
        /// The Ip address from which the user signed in.
        /// </param>
        /// <returns>
        /// The result of the token generation operation, including the generated refresh token if successful.
        /// </returns>
        private async Task<OperationResult<RefreshToken>> GenerateAndSaveRefreshTokenAsync(TradesCoreUser user, string ipAdd)
        {
            try
            {
                var tokenObj = new RefreshToken
                {
                    Token = GenerateRefreshToken(),
                    CreatedByIp = ipAdd,
                    UserId = user.Id
                };

                var oldRefreshToken = await context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (oldRefreshToken is not null) context.Remove(oldRefreshToken);

                await context.RefreshTokens.AddAsync(tokenObj);
                await context.SaveChangesAsync();

                return OperationResult<RefreshToken>.SuccessResult(tokenObj);
            }
            catch (Exception ex)
            {
                return OperationResult<RefreshToken>.Failure($"{tokenCreationFailure} {ex.Message}\nInner Exception: {ex.InnerException}");
            }
        }

        /// <summary>
        /// Method to create an access token for a user using their claims.
        /// </summary>
        /// <param name="claims">
        /// The claims of the user for whom to create the token.
        /// </param>
        /// <returns>
        /// The created access token as a string.
        /// </returns>
        private string CreateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["AppSettings:Issuer"],
                audience: configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
