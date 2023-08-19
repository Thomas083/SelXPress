namespace SelXPressApi.DTO.ProductAttributeDTO
{
    /// <summary>
    /// Data transfer object for creating a new product attribute.
    /// </summary>
    public class CreateProductAttributeDTO
    {
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
