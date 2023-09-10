namespace SelXPressApi.Models
{
	/// <summary>
	/// Represents additional data associated with an Attribute. 
	/// <see cref="DTO.AttributeDataDTO"/>
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
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
