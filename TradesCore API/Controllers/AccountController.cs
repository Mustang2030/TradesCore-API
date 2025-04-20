using Repository_Layer.IRepositories; 
using Microsoft.AspNetCore.Mvc;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserRepo userRepo) : ControllerBase
    {
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
