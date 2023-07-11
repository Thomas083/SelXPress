using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Tag> GetAllTags()
        {
            return _context.Tags.OrderBy(t => t.Id).ToList();
        }
    }
}
