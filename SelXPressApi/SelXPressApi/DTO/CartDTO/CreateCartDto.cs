namespace SelXPressApi.DTO.CartDTO;

/// <summary>
/// Dto to create cart
/// </summary>
public class CreateCartDto
{
    /// <summary>
    /// Product id for the cart
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// User id for the cart
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Quantity of the product for the cart
    /// </summary>
    public int Quantity { get; set; }
}