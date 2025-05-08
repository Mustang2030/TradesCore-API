using Microsoft.AspNetCore.Http;

namespace Service_Layer.UserService
{
    public static class UserService
    {
        public static bool AccessAllowedByEmail(HttpContext context, string email)
        {
            string? accHolderEmail = context.User.Claims.FirstOrDefault(c => c.Type.EndsWith("claims/emailaddress"))?.Value;
            string? role = context.User.Claims.FirstOrDefault(c => c.Type.EndsWith("claims/role"))?.Value;
            return accHolderEmail.Equals(email, StringComparison.OrdinalIgnoreCase) || role == "Admin";
        }

        public static bool AccessAllowedById(HttpContext context, string id)
        {
            string? accHolderId = context.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            string? role = context.User.Claims.FirstOrDefault(c => c.Type.EndsWith("claims/role"))?.Value;
            return accHolderId == id || role == "Admin";
        }
    }
}
