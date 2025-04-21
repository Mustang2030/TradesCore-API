using Data_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminActionsController(ICategoryRepo adminActions) : ControllerBase
    {

        //[HttpPost("add-category")]
        //public IActionResult AddCategory(Category category)
        //{
        //    try
        //    {
        //        var result = adminActions.AddCategoryAsync(category).Result;
        //        if (!result.Success) return BadRequest(result.ErrorMessage);

        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
