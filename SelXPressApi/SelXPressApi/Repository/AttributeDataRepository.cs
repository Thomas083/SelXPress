using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
    /// <summary>
    /// Repository for managing AttributeData.
    /// </summary>
    public class AttributeDataRepository : IAttributeDataRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

        public AttributeDataRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
        {
            _context = context;
            _commonMethods = commonMethods;
            _mapper = mapper;
        }

        /// <summary>
        /// Check if an AttributeData with the specified ID exists.
        /// </summary>
        public async Task<bool> AttributeDataExists(int id)
        {
            return await _context.AttributesData.AnyAsync(x => x.Id == id);
        }

        /// <summary>
        /// Create a new AttributeData.
        /// </summary>
        public async Task<bool> CreateAttributeData(CreateAttributeDataDTO createAttribute)
        {
            var attribute = await _context.Attributes.FindAsync(createAttribute.AttributeId);
            if (attribute != null)
            {
                var newAttributeData = _mapper.Map<AttributeData>(createAttribute);
                newAttributeData.Attribute = attribute;

                await _context.AttributesData.AddAsync(newAttributeData);
                return await _commonMethods.Save();
            }
            return false;
        }

        /// <summary>
        /// Delete an AttributeData by its ID.
        /// </summary>
        public async Task<bool> DeleteAttributeData(int id)
        {
            if (await AttributeDataExists(id))
            {
                await _context.AttributesData.Where(a => a.Id == id).ExecuteDeleteAsync();
                return await _commonMethods.Save();
            }
            return false;
        }

        /// <summary>
        /// Get all AttributeData records.
        /// </summary>
        public async Task<List<AttributeData>> GetAllAttributesData()
        {
            return await _context.AttributesData.OrderBy(a => a.Id).ToListAsync();
        }

        /// <summary>
        /// Get an AttributeData by its ID.
        /// </summary>
        public async Task<AttributeData?> GetAttributeDataById(int id)
        {
            return await _context.AttributesData.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update an existing AttributeData.
        /// </summary>
        public async Task<bool> UpdateAttributeData(int id, UpdateAttributeDataDTO updateAttribute)
        {
            if (!await AttributeDataExists(id))
                return false;

            AttributeData attributeData = await _context.AttributesData
                .Include(ad => ad.Attribute) // Include the Attribute relationship
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attributeData != null)
            {
                if (updateAttribute.Key != null && attributeData.Key != updateAttribute.Key)
                    await _context.AttributesData.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Key, x => updateAttribute.Key));
                if (updateAttribute.Value != null && attributeData.Value != updateAttribute.Value)
                    await _context.AttributesData.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Value, x => updateAttribute.Value));

                if (updateAttribute.AttributeId != 0)
                {
                    // Ensure the attribute exists before updating the relationship
                    var attribute = await _context.Attributes.FirstOrDefaultAsync(a => a.Id == updateAttribute.AttributeId);
                    if (attribute != null)
                    {
                        attributeData.Attribute = attribute;
                    }
                }
            }

            return await _commonMethods.Save();
        }
    }
}
