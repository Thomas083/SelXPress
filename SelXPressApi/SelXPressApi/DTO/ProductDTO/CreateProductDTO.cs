using SelXPressApi.Models;

namespace SelXPressApi.DTO.ProductDTO
{
	/// <summary>
	/// Data Transfer Object for creating a new Product. 
	/// Here you can access to model <see cref="Models.Product"/>. 
	/// The main DTO is <see cref="ProductDTO"/>.
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
	public class CreateProductDTO
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the picture URL of the product.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category to which the product belongs.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets a list of attribute IDs associated with the product.
        /// </summary>
        public List<int> ProductAttributeIds { get; set; } // New property for attribute IDs
        
        
    }
}
