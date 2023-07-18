using Microsoft.AspNetCore.Mvc;

namespace SelXPressApi.DTO.AttributeDTO
{
    public class CreateAttributeDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<ProducesAttribute> producesAttributes { get; set; }
    }
}
