using Repository_Layer.IRepositories; 
using Microsoft.AspNetCore.Mvc;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserRepo userRepo) : ControllerBase
    {
        [HttpPatch("change-email")]
        public async Task<IActionResult> ChangeEmail(string userId, string newEmail)
        {
            try
            {
                var result = await userRepo.UpdateEmailAsync(userId, newEmail);
                if (!result.Success) throw new(result.ErrorMessage);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("confirm-new-email")]
        public async Task<IActionResult> ConfirmNewEmail(string userId, string token, string newEmail)
        {
            try
            {
                var result = await userRepo.ConfirmEmailChangeAsync(userId, token, newEmail);
                if (!result.Success) throw new(result.ErrorMessage);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                var result = await userRepo.DeleteUserAsync(email);
                if (!result.Success) throw new(result.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
