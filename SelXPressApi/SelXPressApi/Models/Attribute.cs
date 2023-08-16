using Microsoft.AspNetCore.Mvc;

namespace SelXPressApi.Models
{
    /// <summary>
    /// Model of the attribute table.
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// The ID of the attribute.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the attribute.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The date and time when the attribute was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the attribute was last modified.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// List of ProductAttribute objects for the Many-to-Many relationship.
        /// </summary>
        public ICollection<ProductAttribute> ProductAttributes { get; set; }

        /// <summary>
        /// Collection of AttributeData objects associated with the attribute.
        /// </summary>
        public ICollection<AttributeData> AttributeData { get; set; }
    }
}
