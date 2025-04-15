using Data_Layer.Models;
using Data_Layer.Utilities;

namespace Repository_Layer.IRepositories
{
    public interface IPublicRepo
    {
        Task<OperationResult<List<Category>>> GetAllCategoriesAsync();
        Task<OperationResult<Category?>> GetCategoryById(string id);
    }
}
