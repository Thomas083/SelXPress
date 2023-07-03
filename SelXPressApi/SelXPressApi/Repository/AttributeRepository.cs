using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly DataContext _context;

        public AttributeRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Attribute> GetAllAttributes()
        {
            return _context.Attributes.OrderBy(a => a.Id).ToList();
        }
    }
}
