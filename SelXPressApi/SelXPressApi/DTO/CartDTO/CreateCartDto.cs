namespace SelXPressApi.DTO.CartDTO;

/// <summary>
/// Dto to create Cart. 
/// Here you can access to model <see cref="Models.Cart"/>. 
/// The main DTO is <see cref="CartDto"/>.
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
public class CreateCartDto
{
    /// <summary>
    /// Product id for the cart
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Quantity of the product for the cart
    /// </summary>
    public int Quantity { get; set; }
}