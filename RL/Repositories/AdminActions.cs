using Data_Layer.Data;
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
        /// <param name="category">
        /// The category data transfer object containing the name of the category to be added.
        /// </param>
        /// <returns>
        /// The result of the operation, indicating success or failure.
        /// </returns>
        public async Task<OperationResult<Category>> AddCategoryAsync(Category category)
        {
            try
            {
                var existingCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (category is not null) throw new($"{category.Name} already exists");

                await context.AddAsync(category);
                await context.SaveChangesAsync();

                return OperationResult<Category>.SuccessResult();
            }
            catch (Exception e)
            {
                return OperationResult<Category>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
        }
    }
}
