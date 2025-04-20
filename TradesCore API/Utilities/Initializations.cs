using static Service_Layer.ResultService.ResultService;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Data_Layer.Models;


/*{
  "firstName": "Test",
  "lastName": "Case",
  "username": null,
  "email": "lukhanyomayekiso98@gmail.com",
  "phoneNumber": "0785263698",
  "role": "Customer"
}*/

namespace TradesCore_API.Utilities
{
    public static class Initializations
    {
        public static void AddDefaultAdminAccs(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TradesCoreUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            string[] roles = { "Admin", "Customer" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }


            var admins = new TradesCoreUser[]
            {
                new()
                {
                    FirstName = "Thabiso",
                    LastName = "Soaisa",
                    Email = "Thabiso@gmail.com",
                    PhoneNumber = "0875636529",
                    Role = "Admin"
                },

                new()
                {
                    FirstName = "Lukhanyo",
                    LastName = "Mayekiso",
                    Email = "Lukhanyo@gmail.com",
                    PhoneNumber = "0739002497",
                    Role = "Admin"
                }
            };

            foreach (var adminUser in admins)
            {
                if (userManager.FindByEmailAsync(adminUser.Email!).Result == null)
                {
                    var result = IdResult(userManager.CreateAsync(adminUser, "Admin101!").Result);
                    if (!result.Success) throw new(result.ErrorMessage);

                    result = IdResult(userManager.AddToRoleAsync(adminUser, adminUser.Role).Result);
                    if (!result.Success) throw new(result.ErrorMessage);

                    result = IdResult(userManager.AddClaimsAsync(adminUser,
                    [
                        new Claim("username", adminUser.UserName!),
                        new Claim("id", adminUser.Id),
                        new Claim(ClaimTypes.Email, adminUser.Email!),
                        new Claim(ClaimTypes.MobilePhone, adminUser.PhoneNumber!),
                        new Claim(ClaimTypes.Role, adminUser.Role)
                    ]).Result);
                    if (!result.Success) throw new(result.ErrorMessage);
                }
            }            
        }
    }
}
