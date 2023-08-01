using Microsoft.AspNetCore.Mvc;

namespace SelXPressApi.Models;


/// <summary>
/// Model of the attribute table
/// </summary>
public class Attribute
{   
    /// <summary>
    /// Id of the attribute
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the attribute
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Type of the attribute
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Date and time of the creation of the attribute
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time of the modification of the attribute
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// List of ProductAttribute object for the Many to Many relationship
    /// </summary>
    public ICollection<ProductAttribute> ProductAttributes { get; set; }
}