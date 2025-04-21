using AutoMapper;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;
using Repository_Layer.Repositories;
using System.Threading.Tasks;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepo categoryRepo, IMapper mapper) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CategoryDto category)
        {
            try
            {
                var result = await categoryRepo.AddCategoryAsync(mapper.Map<Category>(category));
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            try
            {
                var result = categoryRepo.GetCategoryAsync(id).Result;
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
                var result = categoryRepo.UpdateCategoryAsync(mapper.Map<Category>(category)).Result;
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
