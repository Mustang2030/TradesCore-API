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
    public interface IProductRepo
    {
        Task<OperationResult<Product>> AddProductAsync(Product product);
        Task<OperationResult<Product>> GetProductAsync(string id);
        Task<OperationResult<List<Product>>> GetAllProductAsync();
        Task<OperationResult<Product>> UpdateProductAsync(Product productDto);
        Task<OperationResult<Product>> DeleteProductAsync(string id);
    }
}
