using Data_Layer.Entities;
using Data_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminActionsController(IAdminActions adminActions) : ControllerBase
    {
        private Dictionary<string, object> _returnDictionary = [];

        [HttpPost("Category")]
        public IActionResult AddCategory(CategoryDto request)
        {
            try
            {
                _returnDictionary = adminActions.AddCategoryAsync(request).Result;
                if (!(bool)_returnDictionary["Success"]) return BadRequest(_returnDictionary["ErrorMessage"]);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
    }
}
