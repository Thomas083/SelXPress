using System.ComponentModel.DataAnnotations;

namespace SelXPressApi.DTO.AttributeDataDTO
{
    /// <summary>
    /// Data transfer object for creating AttributeData.
    /// </summary>
    public class CreateAttributeDataDTO
    {
        /// <summary>
        /// Gets or sets the key of the AttributeData.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value of the AttributeData.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated Attribute.
        /// </summary>
        [Required]
        public int AttributeId { get; set; }
    }
}
