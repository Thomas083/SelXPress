namespace SelXPressApi.DTO.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public CategoryDTO.CategoryDTO Category { get; set; }
        public int Stock { get; set; }

        public ICollection<ProductAttributeDTO.ProductAttributeDTO> ProductAttributes { get; set; }
        public ICollection<CommentDTO.CommentDTO> Comments { get; set; }
        public ICollection<CartDTO.CartDto> Carts { get; set; }
        public ICollection<OrderProductDTO.OrderProductDTO> OrderProducts { get; set; }
    }
}
