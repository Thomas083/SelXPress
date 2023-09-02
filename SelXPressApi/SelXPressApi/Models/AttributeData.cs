namespace SelXPressApi.Models
{
    /// <summary>
    /// Represents additional data associated with an attribute.
    /// </summary>
    public class AttributeData
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attribute data.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the key associated with the attribute data.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value associated with the attribute data.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the ID of the attribute this data belongs to.
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Gets or sets the attribute this data belongs to.
        /// </summary>
        public Attribute Attribute { get; set; }
    }
}
