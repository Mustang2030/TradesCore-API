using Data_Layer.Models;
using Data_Layer.Utilities;

namespace Repository_Layer.IRepositories
{
    /// <summary>
    /// Interface for category data access.
    /// </summary>
    public interface ICategoryRepo
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
        Task<OperationResult<Category>> AddCategoryAsync(Category category);

        /// <summary>
        /// Retrieves a category with the specified id.
        /// </summary>
        /// <param name="id">
        /// The unique id of the category to retrieve.
        /// </param>
        /// <returns>
        /// The result of the operation, with the category if successful.
        /// </returns>
        Task<OperationResult<Category>> GetCategoryAsync(string id);

        /// <summary>
        /// Retrieves all categories in the database.
        /// </summary>
        /// <returns>
        /// The result of the operation, with a list of categories if successful.
        /// </returns>
        Task<OperationResult<List<Category>>> GetAllCategoriesAsync();

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">
        /// The category object with which to update the existing category.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        Task<OperationResult<Category>> UpdateCategoryAsync(Category category);
        
        /// <summary>
        /// Deletes an existing category.
        /// </summary>
        /// <param name="id">
        /// The unique id of the category to delete.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        Task<OperationResult<Category>> DeleteCategoryAsync(string id);
    }
}
