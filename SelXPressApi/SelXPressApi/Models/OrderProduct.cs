namespace SelXPressApi.Models;

/// <summary>
/// Model of the OrderProduct table
/// </summary>
public class OrderProduct
{
    /// <summary>
    /// Id of the OrderProduct
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Product object of the OrderProduct
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Id of the Product object of the OrderProduct
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Order object of the OrderProduct object
    /// </summary>
    public Order Order { get; set; }

    /// <summary>
    /// Id of the Order object of the OrderProduct object
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Represent the quantity of a product in an order
    /// </summary>
    public int Quantity { get; set; }
    
}