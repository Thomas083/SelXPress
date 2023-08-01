using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.AttributeDTO
{
    public class UpdateAttributeDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<ProductAttribute> ProductAttribute { get; set; }
        public ICollection<AttributeData> AttributeData { get; set; }
    }
}
