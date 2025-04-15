using Data_Layer.DTOs;
using Data_Layer.Utilities;

namespace Repository_Layer.IRepositories
{
    /// <summary>
    /// Interface for admin actions in the e-commerce system.
    /// </summary>
    public interface IAdminActions
    {
        //Task<CategoryDto> AddCategoryAsync(CategoryDto request);
        Task<OperationResult<CategoryDto>> AddCategoryAsync(CategoryDto request);
    }
}
