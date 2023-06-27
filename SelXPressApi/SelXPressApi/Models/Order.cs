namespace SelXPressApi.Models;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; }
    public float TotalPrice { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; }
}