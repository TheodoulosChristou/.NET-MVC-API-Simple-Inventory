using Microsoft.AspNetCore.Mvc;
using SimpleInventory.Domain.DTOs;
using SimpleInventory.Domain.Models;
using SimpleInventory.Domain.Services;

namespace SimpleInventory.Controllers
{
    [Route("Category")]     // URLs: /Category, /Category/Create
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET /Category
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categories = _categoryService.getAllCategories();
            return View(categories);  // Views/Category/Index.cshtml
        }

        // GET /Category/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new Category());
        }

        // POST /Category/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _categoryService.createCategory(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
