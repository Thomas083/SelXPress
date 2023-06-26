namespace SelXPressApi.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }
}