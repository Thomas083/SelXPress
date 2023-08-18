namespace SelXPressApi.DTO.CartDTO;

/// <summary>
/// Dto to create cart for the admin
/// </summary>
public class CreateCartByAdminDto
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