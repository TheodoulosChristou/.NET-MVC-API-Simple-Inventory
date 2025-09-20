using FluentValidation;
using SimpleInventory.Domain.DTOs;

namespace SimpleInventory.Domain.Validations
{
    public class ProductValidator:AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Sku).NotEmpty().WithMessage("Sku is required");

            RuleFor(x => x.Sku).Length(3, 32).WithMessage("Sku characters must be between 3 - 32.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater or equal to zero");

            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater or equal to zero");
        }
    }
}
