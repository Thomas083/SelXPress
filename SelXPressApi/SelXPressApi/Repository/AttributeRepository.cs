using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly DataContext _context;
        private ICommonMethods _commonMethods;

        public AttributeRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        public async Task<bool> AttributeExists(int id)
        {
            return await _context.Attributes.AnyAsync(a => a.Id == id);
        }

        public Task<bool> CreateAttribute(CreateAttributeDTO createAttribute)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAttribute(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Attribute>> GetAllAttributes()
        {
            throw new NotImplementedException();
        }

        public Task<Attribute?> GetAttributeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAttribute(UpdateAttributeDTO updateAttribute)
        {
            throw new NotImplementedException();
        }
    }
}
