using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class AttributeDataRepository : IAttributeDataRepository
    {
        private readonly DataContext _context;
        private ICommonMethods _commonMethods;

        public AttributeDataRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        public async Task<bool> AttributeDataExists(int id)
        {
            return await _context.AttributesData.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CreateAttributeData(CreateAttributeDataDTO createAttribute)
        {
            var attribute = _context.Attributes.Where(a => a.Id == createAttribute.AttributeId).FirstOrDefault();
            if(attribute != null)
            {
                AttributeData newAttributeData = new AttributeData
                {
                    Key = createAttribute.Key,
                    Value = createAttribute.Value,
                    Attribute = attribute
                };
                await _context.AttributesData.AddAsync(newAttributeData);
                return await _commonMethods.Save();
            }
            return false;
        }

        public async Task<bool> DeleteAttributeData(int id)
        {
            if( await AttributeDataExists(id))
            {
                await _context.AttributesData.Where(a => a.Id == id).ExecuteDeleteAsync();
                return await _commonMethods.Save();
            }
            return false;
        }

        public async Task<List<AttributeData>> GetAllAttributesData()
        {
            return await _context.AttributesData.OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<AttributeData?> GetAttributeDataById(int id)
        {
            return await _context.AttributesData.Where(r => r.Id == id).FirstAsync();
        }

        public async Task<bool> UpdateAttributeData(int id, UpdateAttributeDataDTO updateAttribute)
        {
            if(!await AttributeDataExists(id))
                return false;
            AttributeData attributeData = await _context.AttributesData.Where(a => a.Id == id).FirstAsync();

            if (attributeData != null && updateAttribute.Key != null && attributeData.Key != updateAttribute.Key)
                await _context.AttributesData.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Key, x => updateAttribute.Key));
            if(attributeData != null && updateAttribute.Value != null && attributeData.Value != updateAttribute.Value)
                await _context.AttributesData.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Value, x => updateAttribute.Value));
            if (attributeData != null && id != updateAttribute.AttributeId)
                await _context.AttributesData.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Id, x => updateAttribute.AttributeId));
            return await _commonMethods.Save();
        }
    }
}
