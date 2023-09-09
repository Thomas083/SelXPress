namespace SelXPressApi.Models;

/// <summary>
/// Represents a relationship between a user (seller) and a product in the SelXPressApi application. 
/// <see cref="DTO.SellerProductDTO"/>
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
public class SellerProduct
{
	/// <summary>
	/// Gets or sets the unique identifier for the seller-product relationship.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the user (seller) associated with this relationship.
	/// </summary>
	public User User { get; set; }

	/// <summary>
	/// Gets or sets the ID of the user (seller).
	/// </summary>
	public int UserId { get; set; }

	/// <summary>
	/// Gets or sets the product associated with this relationship.
	/// </summary>
	public Product Product { get; set; }

	/// <summary>
	/// Gets or sets the ID of the product.
	/// </summary>
	public int ProductId { get; set; }
}
