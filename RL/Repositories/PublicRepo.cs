using Data_Layer.Data;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Data_Layer.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repositories
{
    public class PublicRepo(TradesCoreDbContext context) : IPublicRepo
    {
        public async Task<OperationResult<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            try 
            {
                var categories = await context.Categories.ToListAsync();
                
                return OperationResult<List<CategoryDto>>.SuccessResult();

            }
            catch (Exception e)
            {
                return OperationResult<List<CategoryDto>>.Failure(e.Message);
            }
        }

        public Task<OperationResult<Category?>> GetCategoryById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
