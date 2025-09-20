using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimpleInventory.Domain.DTOs;
using SimpleInventory.Domain.Models;
using SimpleInventory.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SimpleInventory.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly InventoryDbContext _dbContext;

        private readonly IValidator<ProductDTO> _validator;

        public ProductService(InventoryDbContext dbContext, IValidator<ProductDTO> validatory)
        {
            _dbContext  = dbContext;            
            _validator = validatory;
        }
        public async Task<ProductDTO> createProduct(ProductDTO productRequest)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(productRequest);

                Product pRequest = new Product
                {
                    Sku = productRequest.Sku,
                    Name = productRequest.Name,
                    Price = productRequest.Price,
                    Quantity = productRequest.Quantity,
                    CategoryId = productRequest.CategoryId,
                    UpdatedAt = productRequest.UpdatedAt,
                };

                _dbContext.Product.Add(pRequest);
                await _dbContext.SaveChangesAsync();

                productRequest.Id = pRequest.Id;
                return productRequest;
               
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BaseCommandResponse> deleteProductById(int productId)
        {
            try
            {
                var product = _dbContext.Product.Include(x => x.Category).FirstOrDefault(x => x.Id == productId);

                if(product == null)
                {
                    throw new Exception("Product Not found. Cannot proceed with DELETE Operation");
                } else
                {
                    _dbContext.Product.Remove(product);
                    await _dbContext.SaveChangesAsync();
                    BaseCommandResponse response = new BaseCommandResponse
                    {
                        Id = productId,
                        Entity = "Product",
                        Message = "Product Object has been deleted successfully"
                    };

                    return response;
                }
            } catch( Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductGetResult> getAllProducts(int? categoryId, string? name, string? sku, int page, int pageSize)
        {
            try
            {
                if(page < 1)
                {
                    page = 1;
                }

                if(pageSize < 1)
                {
                    pageSize = 20;
                }

                if(pageSize > 100)
                {
                    pageSize = 100;
                }


            }catch (Exception ex)
            {
                throw ex;
            }

            var query = _dbContext.Product.Include(x => x.Category).ToList();

            var skip = (page - 1) * pageSize;

            if(name != null)
            {
                query = query.Where(x => x.Name.Contains(name)).ToList();
            }

            if(sku != null)
            {
                query = query.Where(x=>x.Sku.Contains(sku)).ToList();
            }

            if(categoryId != null)
            {
                query = query.Where(x => x.CategoryId == categoryId).ToList();
            }

            var finalResult = query.Skip(skip).Take(pageSize).Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku,
                CategoryId = p.CategoryId,
                Price = p.Price,
                Quantity  = p.Quantity,
                UpdatedAt =  p.UpdatedAt
            })
            .ToList();

            var count = finalResult.Count();

            ProductGetResult result = new ProductGetResult
            {
                items = finalResult,
                total = count,
                page = page,
                pageSize = pageSize
            };

            return result;

            
        }

        public ProductDTO getProductById(int productId)
        {
            try
            {
                var product = _dbContext.Product.Include(x=>x.Category).FirstOrDefault(x=>x.Id == productId);
                if (product != null)
                {
                    ProductDTO result = new ProductDTO
                    {
                        Id = product.Id,
                        Sku = product.Sku,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CategoryId = product.CategoryId,
                        UpdatedAt = product.UpdatedAt
                    };

                    return result;
                    
                } else
                {
                    throw new Exception("Product not found in the database");
                }
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDTO> updateProduct(ProductDTO productRequest)
        {
            await _validator.ValidateAndThrowAsync(productRequest);
            Product pRequest = new Product
            {
                Id = productRequest.Id,
                Sku = productRequest.Sku,
                Name = productRequest.Name,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
                CategoryId = productRequest.CategoryId,
                UpdatedAt = productRequest.UpdatedAt
            };

            _dbContext.Product.Update(pRequest);
            await _dbContext.SaveChangesAsync();
            return productRequest;
        }
    }
}
