using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.IRepositories
{
    public interface ISellerActions
    {
        Task<Product> AddProductAsync(P);
    }
}
