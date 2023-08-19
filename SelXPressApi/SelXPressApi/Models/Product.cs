using Microsoft.AspNetCore.Mvc;

namespace SelXPressApi.Models;


/// <summary>
/// Model of the Product table
/// </summary>
public class Product
{
    /// <summary>
    /// Id of the product
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price of the product
    /// </summary>
    public float Price { get; set; }

    /// <summary>
    /// Model of the Product table
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Url of the picture of the product
    /// </summary>
    public string Picture { get; set; }

    /// <summary>
    /// Date and time of the creation of the product
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// category of the product
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Stock of the product
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// List of the cart where the product is in
    /// </summary>
    public ICollection<Cart> Carts { get; set; }

    /// <summary>
    /// List of the order where the product is in
    /// </summary>
    public ICollection<OrderProduct> OrderProducts { get; set; }

    /// <summary>
    /// List of the attributes of the product
    /// </summary>
    public ICollection<ProductAttribute> ProductAttributes { get; set; }
    /// <summary>
    /// List of comments for the product
    /// </summary>
    public ICollection<Comment> Comments { get; set; }
}