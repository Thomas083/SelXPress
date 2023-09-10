namespace SelXPressApi.Models;

/// <summary>
/// Model of the User table. 
/// <see cref="DTO.UserDTO"/>
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
public class User
{
    /// <summary>
    /// Id of the user
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// username of the user (unique)
    /// </summary>
    public string Username { get; set; }

    
    /// <summary>
    /// Email of the user (unique)
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Role of the user
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// List of the product in the user's cart
    /// </summary>
    public ICollection<Cart> Carts { get; set; }
	/// <summary>
	/// Gets or sets a collection of seller-product relationships associated with this user.
	/// </summary>
	public ICollection<SellerProduct> SellerProducts { get; set; }

}