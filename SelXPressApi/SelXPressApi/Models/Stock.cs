namespace SelXPressApi.Models;

public class Stock
{
    public int Id { get; set; }
    public Product StockProduct { get; set; }
    public int Quantity { get; set; }
}