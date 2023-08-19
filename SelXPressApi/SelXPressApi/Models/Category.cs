namespace SelXPressApi.Models
{
    /// <summary>
    /// Model representing a category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the ID of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of tags associated with the category.
        /// </summary>
        public ICollection<Tag> Tags { get; set; }
    }
}
