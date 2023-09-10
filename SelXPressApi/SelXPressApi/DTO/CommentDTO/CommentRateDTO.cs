using SelXPressApi.Models;

namespace SelXPressApi.DTO.CommentDTO
{
	/// <summary>
	/// Represents a data transfer object (DTO) for a Comment. 
	/// Here you can access to model <see cref="Models.Comment"/>. 
	/// The main DTO is <see cref="CommentDTO"/>.
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
	public class CommentRateDTO
	{
		/// <summary>
		/// Id of the comment
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Message of the comment
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Title of the comment
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Date and time of the creation of the comment
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Mark of the comment
		/// </summary>
		public Mark Mark { get; set; }
	}
}
