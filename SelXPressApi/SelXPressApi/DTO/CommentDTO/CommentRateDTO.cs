using SelXPressApi.Models;

namespace SelXPressApi.DTO.CommentDTO
{
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
