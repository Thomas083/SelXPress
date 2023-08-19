namespace SelXPressApi.DTO.TagDTO
{
    /// <summary>
    /// Data Transfer Object for updating a tag.
    /// </summary>
    public class UpdateTagDTO
    {
        /// <summary>
        /// New name for the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the new category for the tag.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
