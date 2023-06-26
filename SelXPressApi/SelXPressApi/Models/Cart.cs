namespace SelXPressApi.Models;

public class Cart
{
    public int Id { get; set; }
    public User CartUser { get; set; }
    public Product CartProduct { get; set; }
}