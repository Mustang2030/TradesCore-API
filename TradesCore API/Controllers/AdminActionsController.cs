using Data_Layer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminActionsController(IAdminActions adminActions) : ControllerBase
    {

        [HttpPost("Category")]
        public IActionResult AddCategory(CategoryDto request)
        {
            try
            {
                var result = adminActions.AddCategoryAsync(request).Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
    }
}
