namespace SelXPressApi.Models;

public class Comment
{
    public int Id { get; set; }
    public string message { get; set; }
    public User UserId { get; set; }
    public Product ProductId { get; set; }
}