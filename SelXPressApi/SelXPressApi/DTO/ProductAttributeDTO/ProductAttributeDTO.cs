namespace SelXPressApi.DTO.ProductAttributeDTO
{
    /// <summary>
    /// Data transfer object for representing a product attribute.
    /// </summary>
    public class ProductAttributeDTO
    {
        /// <summary>
        /// Gets or sets the ID of the product attribute.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product associated with the attribute.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the attribute.
        /// </summary>
        public int AttributeId { get; set; }
    }
}
