using SelXPressApi.DTO.TagDTO;

namespace SelXPressApi.DTO.CategoryDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}
