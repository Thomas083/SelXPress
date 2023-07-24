using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.MarkDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class MarkRepository : IMarkRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;

        public MarkRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        public ICollection<Mark> GetAllMarks()
        {
            return _context.Marks.OrderBy(m => m.Id).ToList();
        }

        public async Task<List<Mark>> GetAllMark()
        {
            return await _context.Marks.OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Mark> GetMarkById(int id)
        {
            return await _context.Marks.Where(m => m.Id == id).FirstAsync();
        }

        public async Task<List<Mark>> GetMarkByUser(int id)
        {
            return await _context.Comments.Where(c => c.User.Id == id).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
                (comment, mark) => new Mark()
                {
                    Id = mark.Id,
                    rate = mark.rate
                }).ToListAsync();
        }

        public async Task<List<Mark>> GetMarkByProduct(int id)
        {
            return await _context.Comments.Where(c => c.Product.Id == id).Join(_context.Marks, comm => comm.Mark.Id,
                mark => mark.Id, (comm, mark) => new Mark()
                {
                    Id = mark.Id,
                    rate = mark.rate
                }).ToListAsync();
        }

        public async Task<bool> CreateMark(CreateMarkDTO mark)
        {
            Mark newMark = new Mark()
            {
                rate = mark.Rate
            };
            await _context.Marks.AddAsync(newMark);
            return await _commonMethods.Save();
        }

        public async Task<bool> UpdateMarkById(UpdateMarkDTO updateMark, int id)
        {
            if (!await MarkExists(id))
                return false;
            var mark = await _context.Marks.Where(m => m.Id == id).FirstAsync();
            if (updateMark.Rate != mark.rate)
            {
                await _context.Marks.Where(m => m.Id == id)
                    .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.rate, x => updateMark.Rate));
            }
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteMarkById(int id)
        {
            if (!await MarkExists(id))
                return false;
            await _context.Marks.Where(m => m.Id == id).ExecuteDeleteAsync();
            return await _commonMethods.Save();
        }

        public async Task<bool> MarkExists(int id)
        {
            return await _context.Marks.Where(m => m.Id == id).AnyAsync();
        }
    }
}
