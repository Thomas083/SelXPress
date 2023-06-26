namespace SelXPressApi.Models;

public class OrderProduct
{
    public int Id { get; set; }
    public Product ProductOrder { get; set; }
    public Order OrderProductOrder { get; set; }
}