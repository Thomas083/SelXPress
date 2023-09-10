using SelXPressApi.DTO.CartDTO;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.DTO.OrderDTOProductDTO;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.ProductDTO
{
	/// <summary>
	/// Represents a data transfer object (DTO) for a Product. 
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
	public class AllProductDTO
	{
		/// <summary>
		/// Gets or sets the unique identifier of the product.
		/// </summary>
		public int Id { get; set; }

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
		/// Gets or sets the URL of the product picture.
		/// </summary>
		public string Picture { get; set; }

		/// <summary>
		/// Gets or sets the available stock quantity of the product.
		/// </summary>
		public int Stock { get; set; }

		/// <summary>
		/// Gets or sets the creation date and time of the product.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the category of the product.
		/// </summary>
		public CategoryDTO.CategoryDTO Category { get; set; }

		/// <summary>
		/// Gets or sets the attributes associated with the product.
		/// </summary>
		public ICollection<ProductAttributeDTO.ProductAttributeDTO> ProductAttributes { get; set; }

		/// <summary>
		/// Gets or sets the comments related to the product.
		/// </summary>
		public ICollection<Comment> Comments { get; set; }
	}
}
