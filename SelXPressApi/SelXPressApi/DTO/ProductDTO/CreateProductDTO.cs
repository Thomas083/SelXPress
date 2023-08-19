namespace SelXPressApi.DTO.ProductDTO
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public List<int> AttributeIds { get; set; } // New property for attribute IDs
    }
}
