namespace SelXPressApi.Models;

public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public ICollection<Category> Categories { get; set; }
}