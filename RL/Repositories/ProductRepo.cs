using Data_Layer.Data;
using Data_Layer.DTOs;
using Data_Layer.Models;
using Data_Layer.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repositories
{
    public class ProductRepo(TradesCoreDbContext context) : IProductRepo
    {
        public async Task<OperationResult<Product>> AddProductAsync(ProductDto product)
        {
            try
            {
                if (await context.Products.AnyAsync(p => p.Name == product.Name))
                    throw new($"{product.Name} already exists");

                var prod = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Stock = product.Stock,
                    Price = product.Price,
                    Categories = product.Categories,
                    ImageUrl = product.ImageUrl,
                };

                await context.AddAsync(prod);
                await context.SaveChangesAsync();

                return OperationResult<Product>.SuccessResult();
            }
            catch (Exception e)
            {
                return OperationResult<Product>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
        }

        public async Task<OperationResult<List<Product>>> GetAllProductAsync()
        {
            try
            {
                var products = await context.Products.ToListAsync();
                return OperationResult<List<Product>>.SuccessResult(products);
            }
            catch (Exception e)
            {
                return OperationResult<List<Product>>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
        }

        public async Task<OperationResult<Product>> GetProductAsync(string id)
        {
            try
            {
                var product = await context.Products.FindAsync(id)
                ?? throw new("Product with this Id was not found.");

                return OperationResult<Product>.SuccessResult(product);
            }
            catch (Exception e)
            {
                return OperationResult<Product>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
            
        }

        public async Task<OperationResult<Product>> UpdateProductAsync(ProductDto product)
        {
            try
            {
                var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id)
                    ?? throw new($"{product.Name} does not exist");


                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Stock = product.Stock;
                    existingProduct.Price = product.Price;
                    existingProduct.Categories = product.Categories;
                    existingProduct.ImageUrl = product.ImageUrl;

                context.Update(existingProduct);
                await context.SaveChangesAsync();

                return OperationResult<Product>.SuccessResult(existingProduct);
            }
            catch (Exception e)
            {
                return OperationResult<Product>.Failure(e.Message + "\nInner Exeption: " + e.InnerException);
            }
        }

        public async Task<OperationResult<Product>> DeleteProductAsync(string id)
        {
            try
            {
                var existingProduct = await context.Products.FindAsync(id)
                    ?? throw new("Product with that Id is not found");

                context.Products.Remove(existingProduct);
                await context.SaveChangesAsync();

                return OperationResult<Product>.SuccessResult();
            }
            catch (Exception ex)
            {
                return OperationResult<Product>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }
    }
}
