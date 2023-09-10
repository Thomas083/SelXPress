namespace SelXPressApi.DTO.AttributeDataDTO
{
	/// <summary>
	/// Data transfer object for updating an AttributeData. 
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
