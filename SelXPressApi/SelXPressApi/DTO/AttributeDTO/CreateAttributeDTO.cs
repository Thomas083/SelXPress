using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.AttributeDTO
{
    /// <summary>
    /// Data transfer object for creating a new Attribute.
    /// </summary>
    public class CreateAttributeDTO
    {
        /// <summary>
        /// Name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the attribute.
        /// </summary>
        public string Type { get; set; }
    }
}
