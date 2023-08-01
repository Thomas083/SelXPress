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

        public async Task<bool> CreateAttribute(CreateAttributeDTO createAttribute)
        {
            Attribute newAttribute = new Attribute
            {
                Name = createAttribute.Name,
                Type = createAttribute.Type,
            };
            await _context.Attributes.AddAsync(newAttribute);
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteAttribute(int id)
        {
            if(await AttributeExists(id))
            {
                await _context.Attributes.Where(a => a.Id == id).ExecuteDeleteAsync();
                return await _commonMethods.Save();
            }
            return false;
        }

        public async Task<List<Attribute>> GetAllAttributes()
        {
            return await _context.Attributes.OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<Attribute?> GetAttributeById(int id)
        {
            return await _context.Attributes.OrderBy(a =>a.Id).FirstAsync();
        }

        public async Task<bool> UpdateAttribute(int id, UpdateAttributeDTO updateAttribute)
        {
            if(!await AttributeExists(id)) 
                return false;
            Attribute attribute = await _context.Attributes.Where(a => a.Id == id).FirstAsync();

            if(attribute != null && updateAttribute.Name != null && attribute.Name != updateAttribute.Name)
                await _context.Attributes.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Name, x => updateAttribute.Name));
            if(attribute != null && updateAttribute.Type != null && attribute.Type != updateAttribute.Type)
                await _context.Attributes.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Type, x => updateAttribute.Type));
            return await _commonMethods.Save();
        }
    }
}
