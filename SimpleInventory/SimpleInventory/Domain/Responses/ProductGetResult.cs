using SimpleInventory.Domain.DTOs;

namespace SimpleInventory.Domain.Responses
{
    public class ProductGetResult
    {
        public List<ProductDTO> items { get; set; }

        public int total { get; set; }

        public int page { get; set; }

        public int pageSize { get; set; }   

    }
}
