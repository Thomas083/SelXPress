using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.Models;


namespace SelXPressApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment> GetCommentById(int id);
        Task<List<Comment>> GetCommentByProduct(int productId);
        Task<List<Comment>> GetCommentByUser(int userId);
        Task<bool> CreateComment(CreateCommentDTO comment);
        Task<bool> UpdateCommentById(UpdateCommentDTO updateComment , int id);
        Task<bool> DeleteCommentById(int id);
        Task<bool> CommentExists(int id);
    }
}
