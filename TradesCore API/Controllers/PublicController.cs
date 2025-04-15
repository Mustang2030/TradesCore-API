﻿using Microsoft.AspNetCore.Mvc;
using Repository_Layer.IRepositories;

namespace TradesCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController(IPublicRepo publicRepo) : Controller
    {
        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var result = publicRepo.GetAllCategoriesAsync().Result;
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
