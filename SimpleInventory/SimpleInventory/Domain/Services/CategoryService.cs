
using Microsoft.EntityFrameworkCore;
using SimpleInventory.Domain.Models;
using SimpleInventory.Domain.Responses;
using System.Threading.Tasks;

namespace SimpleInventory.Domain.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly InventoryDbContext _dbContext;

        public CategoryService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> createCategory(Category category)
        {
            try
            {
                if(category.Name == null)
                {
                    throw new Exception("Category Name must not be null");
                } else
                {
                    _dbContext.Category.Add(category);
                    _dbContext.SaveChanges();
                    return category;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool checkIfCategoryHasProducts(int categoryId)
        {
            try
            {
                var list = _dbContext.Category.Include(x => x.Products).FirstOrDefault(x => x.Id == categoryId);

                bool hasProducts = list?.Products != null && list.Products.Any();
                return hasProducts;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Category> getAllCategories()
        {
            var finalList = _dbContext.Category.Include(x=>x.Products).ToList();
            return finalList;
        }

        public  async Task<BaseCommandResponse> deleteCategoryById(int categoryId)
        {
            try
            {
                var category = _dbContext.Category.FirstOrDefault(x=>x.Id == categoryId);
                _dbContext.Category.Remove(category);
                await _dbContext.SaveChangesAsync();
                BaseCommandResponse response = new BaseCommandResponse
                {
                    Id = categoryId,
                    Entity = "Category",
                    Message = "Category has been successfully deleted"
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
