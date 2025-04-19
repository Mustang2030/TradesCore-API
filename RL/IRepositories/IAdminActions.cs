using Data_Layer.Models;
using Data_Layer.Utilities;

namespace Repository_Layer.IRepositories
{
    /// <summary>
    /// Interface for admin actions in the e-commerce system.
    /// </summary>
    public interface IAdminActions
    {
        //Task<CategoryDto> AddCategoryAsync(CategoryDto request);
        Task<OperationResult<Category>> AddCategoryAsync(Category request);
    }
}
