namespace SelXPressApi.DTO.AttributeDataDTO
{
    /// <summary>
    /// Data transfer object for updating an AttributeData.
    /// </summary>
    public class UpdateAttributeDataDTO
    {
        /// <summary>
        /// New value for the Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// New value for the Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// ID of the new associated Attribute.
        /// </summary>
        public int AttributeId { get; set; }
    }
}
