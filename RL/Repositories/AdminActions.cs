using Data_Layer.Data;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Data_Layer.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IRepositories;

namespace Repository_Layer.Repositories
{
    /// <summary>
    /// Class for admin actions in the e-commerce system.
    /// </summary>
    /// <param name="context">
    /// The database context used for data access.
    /// </param>
    public class AdminActions(TradesCoreDbContext context) : IAdminActions
    {
        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="request">
        /// The category data transfer object containing the name of the category to be added.
        /// </param>
        /// <returns>
        /// The result of the operation, indicating success or failure.
        /// </returns>
        public async Task<OperationResult<CategoryDto>> AddCategoryAsync(CategoryDto request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == request.Name);
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
