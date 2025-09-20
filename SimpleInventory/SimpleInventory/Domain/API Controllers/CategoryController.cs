using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInventory.Domain.Models;
using SimpleInventory.Domain.Responses;
using SimpleInventory.Domain.Services;

namespace SimpleInventory.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;            
        }

        [HttpGet("getAllCategories")]
        public async Task<ActionResult<List<Category>>> getAllCategories()
        {
            var list = _service.getAllCategories();
            return Ok(list);    
        }

        [HttpPost("createCategory")]
        public async Task<ActionResult<Category>> createCategory([FromBody]Category category)
        {
            var response = await _service.createCategory(category);
            return Ok(response);
        }

        [HttpDelete("categories/{categoryId:int}")]
        public async Task<ActionResult<BaseCommandResponse>> deleteCategory(int categoryId)
        {
            var response = _service.checkIfCategoryHasProducts(categoryId);

            if(response == true)
            {
                return Conflict("" +
                    "Cannot delete the category because it has products assigned on it");
            } else
            {
                var finalRes = await _service.deleteCategoryById(categoryId);
                return Ok(finalRes);
            }
                
        }
    }
}

