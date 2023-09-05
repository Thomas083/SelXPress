using SelXPressApi.Models;

namespace SelXPressApi.DTO.ProductDTO
{
    /// <summary>
    /// Data Transfer Object for updating a product.
    /// </summary>
    public class UpdateProductDTO
    {
        /// <summary>
        /// Gets or sets the updated name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the updated price of the product.
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Gets or sets the updated description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the updated picture URL of the product.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the updated stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the updated category of the product.
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Gets or sets a list of attribute Ids associated with the product
        /// </summary>
        public List<int> AttributeIds { get; set; }
    }
}
