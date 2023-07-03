using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class MarkRepository : IMarkRepository
    {
        private readonly DataContext _context;

        public MarkRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Mark> GetAllMarks()
        {
            return _context.Marks.OrderBy(m => m.Id).ToList();
        }
    }
}
