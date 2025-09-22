using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleInventory.Domain.DTOs;
using SimpleInventory.Domain.Services;

namespace SimpleInventory.Controllers
{
    [Route("Prod")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.getAllProducts(null, null, null, 1, 10);
            return View("~/Views/Product/Index.cshtml", result);
        }


        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View("~/Views/Product/Create.cshtml", new ProductDTO { UpdatedAt = DateTime.UtcNow });
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO model)
        {
            if (model.CategoryId <= 0)
                ModelState.AddModelError(nameof(model.CategoryId), "Please select a category.");

            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View("~/Views/Product/Create.cshtml", model);
            }

            await _productService.createProduct(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = _productService.getProductById(id); 
            if (product == null) return NotFound();

            await LoadCategoriesAsync(product.CategoryId);
            return View("~/Views/Product/Edit.cshtml", product);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDTO model)
        {
            if (id != model.Id)
                return BadRequest("Route id and model id do not match.");

            if (model.CategoryId <= 0)
                ModelState.AddModelError(nameof(model.CategoryId), "Please select a category.");

            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync(model.CategoryId);
                return View("~/Views/Product/Edit.cshtml", model);
            }

            await _productService.updateProduct(model);
            return RedirectToAction(nameof(Index));
        }

        // ---------- DELETE ----------
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _productService.getProductById(id);
            if (product == null) return NotFound();

            return View("~/Views/Product/Delete.cshtml", product);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.deleteProductById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var product = _productService.getProductById(id);
                if (product == null) return RedirectToAction(nameof(Index));
                return View("~/Views/Product/Delete.cshtml", product);
            }
        }

        private async Task LoadCategoriesAsync(int? selectedId = null)
        {
            var cats = _categoryService.getAllCategories();

            ViewBag.Categories = cats
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = selectedId.HasValue && c.Id == selectedId.Value
                })
                .Prepend(new SelectListItem { Text = "-- select category --", Value = "" })
                .ToList();
        }
    }
}
