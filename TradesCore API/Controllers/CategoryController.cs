using Data_Layer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;
using Repository_Layer.Repositories;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepo categoryRepo) : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult Add(CategoryDto category)
        {
            try
            {
                var result = categoryRepo.AddCategoryAsync(category).Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(CategoryDto category)
        {
            try
            {
                var result = categoryRepo.GetCategoryAsync(category.Id).Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = categoryRepo.GetAllCategoriesAsync().Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult AddProduct(CategoryDto category)
        {
            try
            {
                var result = categoryRepo.UpdateCategoryAsync(category).Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(CategoryDto product)
        {
            try
            {
                var result = categoryRepo.DeleteCategoryAsync(product.Id).Result;
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
