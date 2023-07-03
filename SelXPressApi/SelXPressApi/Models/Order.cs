namespace SelXPressApi.Models;

/// <summary>
/// Model of the Order table
/// </summary>
public class Order
{
    /// <summary>
    /// Id of the order
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Total price of the order
    /// </summary>
    public float TotalPrice { get; set; }

    /// <summary>
    /// Date and time of the creation of the order
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// User's order
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// List of the product of the order
    /// </summary>
    public ICollection<OrderProduct> OrderProducts { get; set; }
}