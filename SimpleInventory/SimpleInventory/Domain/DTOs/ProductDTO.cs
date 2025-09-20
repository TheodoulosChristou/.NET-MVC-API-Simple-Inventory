namespace SimpleInventory.Domain.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }
    }
}
