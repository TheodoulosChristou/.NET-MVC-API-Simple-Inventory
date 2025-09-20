using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInventory.Domain.DTOs;
using SimpleInventory.Domain.Responses;
using SimpleInventory.Domain.Services;

namespace SimpleInventory.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet("products")]
        public async Task<ActionResult<ProductGetResult>> getAllProducts([FromQuery] int? categoryId, [FromQuery] string? name, [FromQuery] string? sku, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var list = await _productService.getAllProducts(categoryId, name, sku, page, pageSize); 
            return Ok(list);
        }

        [HttpGet("products/{productId:int}")]
        public async Task<ActionResult<ProductDTO>> getAllProductById(int productId)
        {
            var list = _productService.getProductById(productId);
            return Ok(list);
        }

        [HttpPost("products")]
        public async Task<ActionResult<ProductDTO>> createProduct([FromBody]ProductDTO productRequest)
        {
            var result = await _productService.createProduct(productRequest);
            return Ok(result);
        }

        [HttpPut("products")]
        public async Task<ActionResult<ProductDTO>> updateProduct([FromBody] ProductDTO productRequest)
        {
            var result = await _productService.updateProduct(productRequest);
            return Ok(result);
        }

        [HttpDelete("products/{productId:int}")]
        public async Task<ActionResult<BaseCommandResponse>> deleteProduct(int productId)
        {
            var result = await _productService.deleteProductById(productId);
            return Ok(result);
        }

    }
}
