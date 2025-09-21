using System.ComponentModel.DataAnnotations;

namespace SimpleInventory.Domain.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(32)]
        public string Sku { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";

        [Range(0, double.MaxValue, ErrorMessage = "Price must be ≥ 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be ≥ 0")]
        public int Quantity { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
    }
}
