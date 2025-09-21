using Microsoft.AspNetCore.Mvc;
using SimpleInventory.Domain.DTOs;
using SimpleInventory.Domain.Services;

namespace SimpleInventory.Controllers
{
    [Route("Prod")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /Prod
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.getAllProducts(
                null, null, null, 1, 10);

            return View("~/Views/Product/Index.cshtml", result);
        }

        // GET /Prod/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            // If you want UpdatedAt prefilled:
            return View("~/Views/Product/Create.cshtml", new ProductDTO { UpdatedAt = DateTime.UtcNow });
        }

        // POST /Prod/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Product/Create.cshtml", model);
            }

            await _productService.createProduct(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = _productService.getProductById(id);
            if (product == null)
                return NotFound();

            return View("~/Views/Product/Edit.cshtml", product);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDTO model)
        {
            if (id != model.Id)
                return BadRequest("Route id and model id do not match.");

            if (!ModelState.IsValid)
                return View("~/Views/Product/Edit.cshtml", model);

            await _productService.updateProduct(model);  // await!
            return RedirectToAction(nameof(Index));
        }

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
    }
}
