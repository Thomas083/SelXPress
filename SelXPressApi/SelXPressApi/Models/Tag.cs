namespace SelXPressApi.Models;

public class Tag
{
    public int Id { get; set; }
    public string TagName { get; set; }
    public Category TagCategory { get; set; }
}