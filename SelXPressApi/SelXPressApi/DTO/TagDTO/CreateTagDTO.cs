namespace SelXPressApi.DTO.TagDTO
{
    /// <summary>
    /// Data transfer object for creating a new tag.
    /// </summary>
    public class CreateTagDTO
    {
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
