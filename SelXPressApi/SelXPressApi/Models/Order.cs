namespace SelXPressApi.Models;

/// <summary>
/// Model of the Order table. 
/// <see cref="DTO.OrderDTO"/>
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