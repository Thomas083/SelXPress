using Microsoft.AspNetCore.Mvc;

namespace SelXPressApi.DTO.AttributeDTO
{
    public class AttributeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<ProducesAttribute> producesAttributes { get; set; }
    }
}
