using SelXPressApi.Models;

namespace SelXPressApi.DTO.ProductDTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public Category Category { get; set; }
        public Stock Stock { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}