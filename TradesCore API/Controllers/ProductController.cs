using Data_Layer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepo productRepo) : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult Add(ProductDto product)
        {
            try
            {
                var result = productRepo.AddProductAsync(product).Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(ProductDto product)
        {
            try
            {
                var result = productRepo.GetProductAsync(product.Id).Result;
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
        public IActionResult Update(ProductDto product)
        {
            try
            {
                var result = productRepo.UpdateProductAsync(product).Result;
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(ProductDto product)
        {
            try
            {
                var result = productRepo.DeleteProductAsync(product.Id).Result;
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
