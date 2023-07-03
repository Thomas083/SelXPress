namespace SelXPressApi.Models;

/// <summary>
/// Model of the Tag table
/// </summary>
public class Tag
{
    /// <summary>
    /// Id of the tag
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the tag
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Category of the Tag
    /// </summary>
    public Category Category { get; set; }
}