using Data_Layer.DTOs;
using Data_Layer.Utilities;

namespace Repository_Layer.IRepositories
{
    public interface IAdminActions
    {
        //Task<CategoryDto> AddCategoryAsync(CategoryDto request);
        Task<OperationResult<CategoryDto>> AddCategoryAsync(CategoryDto request);
    }
}
