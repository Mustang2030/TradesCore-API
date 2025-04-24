using Repository_Layer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Data_Layer.Models;
using Data_Layer.DTOs;
using AutoMapper;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepo productRepo, IMapper mapper) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductDto product)
        {
            try
            {
                var result = await productRepo.AddProductAsync(mapper.Map<Product>(product));
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await productRepo.GetProductAsync(id);
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
                var result = productRepo.GetAllProductAsync().Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Product product)
        {
            try
            {
                var result = await productRepo.UpdateProductAsync(product);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            try
            {
                var result = productRepo.DeleteProductAsync(id).Result;
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
