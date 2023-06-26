namespace SelXPressApi.Models;

public class Order
{
    public int Id { get; set; }
    public User OrderUser { get; set; }
    public float TotalPrice { get; set; }
}