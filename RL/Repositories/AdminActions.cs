using Data_Layer.Data;
using Data_Layer.Entities;
using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IRepositories;

namespace Repository_Layer.Repositories
{
    public class AdminActions(TradesCoreDbContext context) : IAdminActions
    {
        private Dictionary<string, object> _returnDictionary = [];

        public async Task<Dictionary<string,object>> AddCategoryAsync(CategoryDto request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == c.Name);
                if (category is not null) throw new($"{request.Name} already exists");

                var newCat = new Category
                {
                    Name = request.Name
                };

                await context.AddAsync(newCat);
                await context.SaveChangesAsync();

                _returnDictionary["Success"] = true;
                return _returnDictionary;
            }
            catch (Exception e)
            {
                _returnDictionary["Success"] = false;
                _returnDictionary["ErrorMessage"] = e.Message + "\nInner Exception: " + e.InnerException;
                return _returnDictionary;
            }
        }
    }
}
