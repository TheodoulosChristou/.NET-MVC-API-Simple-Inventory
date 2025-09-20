using SimpleInventory.Domain.DTOs;
using SimpleInventory.Domain.Responses;

namespace SimpleInventory.Domain.Services
{
    public interface IProductService
    {

        public Task<ProductGetResult> getAllProducts(int? categoryId, string? name, string? sku, int page, int pageSize);
        public ProductDTO getProductById(int  productId);

        public Task<ProductDTO> createProduct(ProductDTO productRequest);

        public Task<ProductDTO> updateProduct(ProductDTO productRequest);    

        public Task<BaseCommandResponse> deleteProductById(int productId);
    }
}
