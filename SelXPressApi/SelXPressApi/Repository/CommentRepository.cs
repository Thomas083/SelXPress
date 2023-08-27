using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;

        public CommentRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }


        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
                (comment, mark) => new Comment()
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    CreatedAt = comment.CreatedAt,
                    Mark = mark,
                    User = comment.User,
                    Product = comment.Product
                }).ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments.Where(c => c.Id == id).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
                (comment, mark) => new Comment()
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    CreatedAt = comment.CreatedAt,
                    Mark = mark,
                    User = comment.User,
                    Product = comment.Product
                }).FirstAsync();
        }

        public async Task<List<Comment>> GetCommentByProduct(int productId)
        {
            return await _context.Comments.Where(c => c.Product.Id == productId).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
                (comment, mark) => new Comment()
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    CreatedAt = comment.CreatedAt,
                    Mark = mark,
                    User = comment.User,
                    Product = comment.Product
                }).ToListAsync();
        }

        public async Task<List<Comment>> GetCommentByUser(int userId)
        {
            return await _context.Comments.Where(c => c.User.Id == userId).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
                (comment, mark) => new Comment()
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    CreatedAt = comment.CreatedAt,
                    Mark = mark,
                    User = comment.User,
                    Product = comment.Product
                }).ToListAsync();
        }

        public async Task<bool> CreateComment(CreateCommentDTO commentDto)
        {
            var user = await _context.Users.Where(u => u.Id == commentDto.UserId).FirstAsync();
            var product = await _context.Products.Where(p => p.Id == commentDto.ProductId).FirstAsync();
            Mark mark = new Mark()
            {
                rate = commentDto.Rate
            };
            await _context.Marks.AddAsync(mark);
            await _commonMethods.Save();
            Comment comment = new Comment()
            {
                Message = commentDto.Message,
                CreatedAt = DateTime.Now,
                Mark = mark,
                Product = product,
                User = user
            };
            await _context.Comments.AddAsync(comment);

            return await _commonMethods.Save();
        }

        public async Task<bool> UpdateCommentById(UpdateCommentDTO updateComment, int id)
        {
            var comment = _context.Comments.Where(c => c.Id == id).FirstAsync();
            if (updateComment.Message != null)
            {
                await _context.Comments.Where(c => c.Id == id)
                    .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Message, x => updateComment.Message));
            }
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteCommentById(int id)
        {
            await _context.Comments.Where(c => c.Id == id).ExecuteDeleteAsync();
            return await _commonMethods.Save();
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.Where(c => c.Id == id).AnyAsync();
        }
    }
}
