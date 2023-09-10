namespace SelXPressApi.DTO.AttributeDataDTO
{
	/// <summary>
	/// Data transfer object for creating AttributeData. 
	/// Here you can access to model <see cref="Models.AttributeData"/>. 
	/// The main DTO is <see cref="AttributeDataDto"/>.
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
	public class CreateAttributeDataDTO
    {
        /// <summary>
        /// Gets or sets the key of the AttributeData.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value of the AttributeData.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated Attribute.
        /// </summary>
        public int AttributeId { get; set; }
    }
}
