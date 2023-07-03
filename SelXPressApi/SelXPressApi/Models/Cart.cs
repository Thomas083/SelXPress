namespace SelXPressApi.Models;

/// <summary>
/// Model of the Cart table
/// </summary>
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
}