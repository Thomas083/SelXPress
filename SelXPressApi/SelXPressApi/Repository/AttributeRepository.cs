using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository class for managing attributes and their data.
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
		/// Checks if an attribute with the given ID exists.
		/// </summary>
		/// <param name="id">The ID of the attribute to check.</param>
		/// <returns>True if the attribute exists; otherwise, false.</returns>
		public async Task<bool> AttributeExists(int id)
        {
            return await _context.Attributes.AnyAsync(a => a.Id == id);
        }

		/// <summary>
		/// Creates a new attribute using the provided data.
		/// </summary>
		/// <param name="createAttribute">Data to create a new attribute.</param>
		/// <returns>True if the attribute was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateAttribute(CreateAttributeDTO createAttribute)
        {
            var newAttribute = new Attribute
            {
                Name = createAttribute.Name,
                Type = createAttribute.Type,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Attributes.Add(newAttribute);
            return await _commonMethods.Save();
        }

		/// <summary>
		/// Deletes an attribute by its ID if it exists.
		/// </summary>
		/// <param name="id">The ID of the attribute to delete.</param>
		/// <returns>True if the attribute was deleted successfully; otherwise, false.</returns>
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
		/// Retrieves all attributes along with their associated attribute data.
		/// </summary>
		/// <returns>A list of attributes with their data.</returns>
		public async Task<List<Attribute>> GetAllAttributes()
        {
            return await _context.Attributes
               .Include(a => a.AttributeData) // Include related AttributeData
               .ToListAsync();
        }

		/// <summary>
		/// Retrieves an attribute by its ID along with its associated attribute data.
		/// </summary>
		/// <param name="id">The ID of the attribute to retrieve.</param>
		/// <returns>The attribute and its associated data, or null if not found.</returns>
		public async Task<Attribute?> GetAttributeById(int id)
        {
            return await _context.Attributes
                .Include(attribute => attribute.AttributeData) // Include associated attribute data
                .FirstOrDefaultAsync(attribute => attribute.Id == id);
        }

		/// <summary>
		/// Updates an existing attribute with the provided data.
		/// </summary>
		/// <param name="id">The ID of the attribute to update.</param>
		/// <param name="updateAttribute">Updated data for the attribute.</param>
		/// <returns>True if the attribute was updated successfully; otherwise, false.</returns>
		public async Task<bool> UpdateAttribute(int id, UpdateAttributeDTO updateAttribute)
        {
            if (!await AttributeExists(id))
                return false;
            var attribute = await _context.Attributes.FirstOrDefaultAsync(a => a.Id == id);

            if (attribute != null)
            {
	            if (updateAttribute.Name != null && attribute.Name != updateAttribute.Name)
	            {
		            await _context.Attributes.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Name, x => updateAttribute.Name));
		            await _context.Attributes.Where(a => a.Id == id)
			            .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.UpdatedAt, x => DateTime.Now));
	            }
                
	            if (updateAttribute.Type != null && attribute.Type != updateAttribute.Type)
	            {
		            await  _context.Attributes.Where(a => a.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Type, x => updateAttribute.Type));
		            await _context.Attributes.Where(a => a.Id == id)
			            .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.UpdatedAt, x => DateTime.Now));
	            }

                return await _commonMethods.Save();
            }
            return false;
        }
    }
}
