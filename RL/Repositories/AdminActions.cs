using Data_Layer.Data;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Data_Layer.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IRepositories;

namespace Repository_Layer.Repositories
{
    public class AdminActions(TradesCoreDbContext context) : IAdminActions
    {

        public async Task<OperationResult<CategoryDto>> AddCategoryAsync(CategoryDto request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == c.Name);
                if (category is not null) throw new($"{request.Name} already exists");

                var newCat = new Category
                {
                    Name = request.Name
                };

                await context.AddAsync(newCat);
                await context.SaveChangesAsync();

                return OperationResult<CategoryDto>.SuccessResult();
            }
            catch (Exception e)
            {
                return OperationResult<CategoryDto>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
        }
    }
}
