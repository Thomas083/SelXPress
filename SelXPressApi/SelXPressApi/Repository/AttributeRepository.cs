using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Repository
{
    /// <summary>
    /// Repository for managing Attribute data.
    /// </summary>
    public class AttributeRepository : IAttributeRepository
    {
        private readonly DataContext _context;
        private ICommonMethods _commonMethods;

        public AttributeRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        /// <summary>
        /// Check if an attribute with the given ID exists.
        /// </summary>
        public async Task<bool> AttributeExists(int id)
        {
            return await _context.Attributes.AnyAsync(a => a.Id == id);
        }

        /// <summary>
        /// Create a new attribute with the provided data.
        /// </summary>
        public async Task<bool> CreateAttribute(CreateAttributeDTO createAttribute)
        {
            var newAttribute = new Attribute
            {
                Name = createAttribute.Name,
                Type = createAttribute.Type,
            };
            _context.Attributes.Add(newAttribute);
            return await _commonMethods.Save();
        }

        /// <summary>
        /// Delete an attribute by its ID if it exists.
        /// </summary>
        public async Task<bool> DeleteAttribute(int id)
        {
            if (await AttributeExists(id))
            {
                _context.Attributes.Where(a => a.Id == id).ExecuteDelete();
                return await _commonMethods.Save();
            }
            return false;
        }

        /// <summary>
        /// Get all attributes along with their associated attribute data.
        /// </summary>
        public async Task<List<Attribute>> GetAllAttributes()
        {
            return await _context.Attributes
               .Include(a => a.AttributeData) // Include related AttributeData
               .ToListAsync();
        }

        /// <summary>
        /// Get an attribute by its ID along with its associated attribute data.
        /// </summary>
        public async Task<Attribute?> GetAttributeById(int id)
        {
            return await _context.Attributes
                .Include(attribute => attribute.AttributeData) // Include associated attribute data
                .FirstOrDefaultAsync(attribute => attribute.Id == id);
        }

        /// <summary>
        /// Update an existing attribute with the provided data.
        /// </summary>
        public async Task<bool> UpdateAttribute(int id, UpdateAttributeDTO updateAttribute)
        {
            if (!await AttributeExists(id))
                return false;
            var attribute = await _context.Attributes.FirstOrDefaultAsync(a => a.Id == id);

            if (attribute != null)
            {
                if (updateAttribute.Name != null && attribute.Name != updateAttribute.Name)
                    _context.Attributes.Where(a => a.Id == id).ExecuteUpdate(p1 => p1.SetProperty(x => x.Name, x => updateAttribute.Name));

                if (updateAttribute.Type != null && attribute.Type != updateAttribute.Type)
                    _context.Attributes.Where(a => a.Id == id).ExecuteUpdate(p1 => p1.SetProperty(x => x.Type, x => updateAttribute.Type));

                return await _commonMethods.Save();
            }
            return false;
        }
    }
}
