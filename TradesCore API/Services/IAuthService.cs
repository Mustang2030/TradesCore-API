using TradesCore_API.Entities;
using TradesCore_API.Models;

namespace TradesCore_API.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}
