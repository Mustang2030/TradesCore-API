using Data_Layer.Data;
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
        public async Task<OperationResult<List<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await context.Categories.ToListAsync();

                return OperationResult<List<Category>>.SuccessResult(categories);
            }
            catch (Exception e)
            {
                return OperationResult<List<Category>>.Failure(e.Message);
            }
            
        }
    }
}
