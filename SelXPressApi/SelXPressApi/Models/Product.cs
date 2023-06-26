namespace SelXPressApi.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public Category ProductCategory { get; set; }
    public string Picture { get; set; }
    public DateTime CreatedAt { get; set; }
    public User SellPeople { get; set; }
    
    //promotion Id <optional>
}