using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Comment> GetAllComments()
        {
            return _context.Comments.OrderBy(c => c.Id).ToList();
        }
    }
}
