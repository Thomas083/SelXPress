namespace SelXPressApi.DTO.CategoryDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagDTO> Tags { get; set; }
    }
}
