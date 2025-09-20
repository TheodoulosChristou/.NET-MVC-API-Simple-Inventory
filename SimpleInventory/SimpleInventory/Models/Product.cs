using Microsoft.AspNetCore.Server.HttpSys;

namespace SimpleInventory.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }    

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
