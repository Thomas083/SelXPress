namespace SelXPressApi.Models;

/// <summary>
/// Model of the Stock table
/// </summary>
public class Stock
{
    /// <summary>
    /// Id of the stock
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Number of the product remains
    /// </summary>
    public int Quantity { get; set; }

}