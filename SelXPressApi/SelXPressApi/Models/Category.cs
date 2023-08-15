namespace SelXPressApi.Models;

/// <summary>
/// Model of the category table
/// </summary>
public class Category
{
    /// <summary>
    /// Id of the category
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the category
    /// </summary>
    public string Name { get; set; }

    public ICollection<Tag> Tags { get; set; }

}