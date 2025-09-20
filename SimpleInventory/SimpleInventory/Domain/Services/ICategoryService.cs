using SimpleInventory.Domain.Models;
using SimpleInventory.Domain.Responses;

namespace SimpleInventory.Domain.Services
{
    public interface ICategoryService
    {

        public List<Category> getAllCategories();

        public Task<Category> createCategory(Category category);

        public bool checkIfCategoryHasProducts(int categoryId);

        public Task<BaseCommandResponse> deleteCategoryById(int categoryId);
    }
}
