using Data_Layer.Data;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Data_Layer.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IRepositories;

namespace Repository_Layer.Repositories
{
    /// <summary>
    /// Repository class for category data access.
    /// </summary>
    /// <param name="context">
    /// The database context used for data access.
    /// </param>
    public class CategoryRepo(TradesCoreDbContext context) : ICategoryRepo
    {
        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">
        /// The <see cref="Category"/> object to add.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public async Task<OperationResult<Category>> AddCategoryAsync(CategoryDto category)
        {
            try
            {
                if (await context.Categories.AnyAsync(c => c.Name == category.Name))
                    throw new($"{category.Name} already exists");

                var cat = new Category
                {
                    Name = category.Name
                };

                await context.AddAsync(cat);
                await context.SaveChangesAsync();

                return OperationResult<Category>.SuccessResult();
            }
            catch (Exception e)
            {
                return OperationResult<Category>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
        }

        /// <summary>
        /// Retrieves a category with the specified id.
        /// </summary>
        /// <param name="id">
        /// The unique id of the category to retrieve.
        /// </param>
        /// <returns>
        /// The result of the operation, with the category if successful.
        /// </returns>
        public async Task<OperationResult<Category>> GetCategoryAsync(string id)
        {
            try
            {
                var category = await context.Categories.FindAsync(id) 
                    ?? throw new("Could not find a category with the specified ID.");

                return OperationResult<Category>.SuccessResult(category);
            }
            catch (Exception ex)
            {
                return OperationResult<Category>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Retrieves all categories stored in the database.
        /// </summary>
        /// <returns>
        /// The result of the operation, with a list of all categories if successful.
        /// </returns>
        public async Task<OperationResult<List<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return OperationResult<List<Category>>.SuccessResult(categories);
            }
            catch (Exception ex)
            {
                return OperationResult<List<Category>>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">
        /// The category object with which to update the existing category.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public async Task<OperationResult<Category>> UpdateCategoryAsync(CategoryDto category)
        {
            try
            {
                var existingCategory = await context.Categories.FindAsync(category.Id)
                    ?? throw new("Could not find a category with the specified ID.");

                existingCategory.Name = category.Name;

                context.Update(existingCategory);
                await context.SaveChangesAsync();

                return OperationResult<Category>.SuccessResult(existingCategory);
            }
            catch (Exception ex)
            {
                return OperationResult<Category>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Deletes an existing category.
        /// </summary>
        /// <param name="id">
        /// The unique id of the category to delete.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public async Task<OperationResult<Category>> DeleteCategoryAsync(string id)
        {
            try
            {
                var existingCategory = await context.Categories.FindAsync(id)
                    ?? throw new("Could not find a category with the specified ID.");
                
                context.Categories.Remove(existingCategory);
                await context.SaveChangesAsync();

                return OperationResult<Category>.SuccessResult();
            }
            catch (Exception ex)
            {
                return OperationResult<Category>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }
    }
}
