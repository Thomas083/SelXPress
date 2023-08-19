using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.AttributeDTO
{
    /// <summary>
    /// Data transfer object for updating an Attribute.
    /// </summary>
    public class UpdateAttributeDTO
    {
        /// <summary>
        /// Updated name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Updated type of the attribute.
        /// </summary>
        public string Type { get; set; }
    }
}
