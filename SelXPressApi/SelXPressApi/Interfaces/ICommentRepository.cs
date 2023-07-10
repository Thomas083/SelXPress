using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ICommentRepository
    {
        ICollection<Comment> GetAllComments();
    }
}
