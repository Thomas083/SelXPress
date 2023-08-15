namespace SelXPressApi.DTO.TagDTO
{
    /// <summary>
    /// Data transfer object for a tag.
    /// </summary>
    public class TagDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the tag.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category associated with the tag.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
