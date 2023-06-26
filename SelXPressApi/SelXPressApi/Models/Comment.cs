namespace SelXPressApi.Models;

public class Comment
{
    public int Id { get; set; }
    public string message { get; set; }
    public User CommentUser { get; set; }
    public Product CommentProduct { get; set; }
}