using Data_Layer.Entities;
using Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.IRepositories
{
    public interface IAdminActions
    {
        //Task<CategoryDto> AddCategoryAsync(CategoryDto request);
        Task<Dictionary<string, object>> AddCategoryAsync(CategoryDto request);

    }
}
