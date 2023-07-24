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

        public async Task<bool> CreateComment(CreateCommentDTO comment)
        {
            //todo

            return await _commonMethods.Save();
        }

        public async Task<bool> UpdateCommentById(UpdateCommentDTO updateComment, int id)
        {
            //todo
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteCommentById(int id)
        {
            //todo
            return await _commonMethods.Save();
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.Where(c => c.Id == id).AnyAsync();
        }
    }
}
