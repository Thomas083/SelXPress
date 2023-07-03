namespace SelXPressApi.Models;

/// <summary>
/// Model of the User table
/// </summary>
public class User
{
    /// <summary>
    /// Id of the user
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// username of the user (unique)
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// password of the user (crypt)
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Email of the user (unique)
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Role of the user
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// List of the product in the user's cart
    /// </summary>
    public ICollection<Cart> Carts { get; set; }
}