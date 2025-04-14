using Data_Layer.DTOs;
using Data_Layer.Models;
using Data_Layer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.IRepositories
{
    public interface IPublicRepo
    {
        Task<OperationResult<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<OperationResult<Category?>> GetCategoryById(string id);
    }
}
