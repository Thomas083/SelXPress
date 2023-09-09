namespace SelXPressApi.Models;

/// <summary>
/// Model of the Cart table. 
/// <see cref="DTO.CartDTO"/>
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
public class Cart
{
    /// <summary>
    /// Id of the cart
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the user's cart
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// User object for the user's cart
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Product object for the product's cart
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Id of the product in the cart
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Represent the quantity of a product in the cart
    /// </summary>
    public int Quantity { get; set; }
}