﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Data;
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

		/// <summary>
		/// Initializes a new instance of the AttributeDataRepository class.
		/// </summary>
		/// <param name="context">The DataContext.</param>
		/// <param name="commonMethods">Common methods helper.</param>
		/// <param name="mapper">AutoMapper instance.</param>
		public AttributeDataRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
		{
			_context = context;
			_commonMethods = commonMethods;
			_mapper = mapper;
		}

		/// <summary>
		/// Checks if an AttributeData record with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the AttributeData to check.</param>
		/// <returns>True if an AttributeData with the specified ID exists; otherwise, false.</returns>
		public async Task<bool> AttributeDataExists(int id)
		{
			return await _context.AttributesData.AnyAsync(x => x.Id == id);
		}

		/// <summary>
		/// Creates a new AttributeData record using the provided data.
		/// </summary>
		/// <param name="createAttribute">The data to create the AttributeData.</param>
		/// <returns>True if the creation is successful; otherwise, false.</returns>
		public async Task<bool> CreateAttributeData(CreateAttributeDataDTO createAttribute)
		{
            AttributeData newAttributeData = new AttributeData
            {
                AttributeId = createAttribute.AttributeId,
				Value = createAttribute.Value,
				Key = createAttribute.Key
            };
            await _context.AttributesData.AddAsync(newAttributeData);
            return await _commonMethods.Save();
        }

		/// <summary>
		/// Deletes an AttributeData record by its ID if it exists.
		/// </summary>
		/// <param name="id">The ID of the AttributeData to delete.</param>
		/// <returns>True if the deletion is successful; otherwise, false.</returns>
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
		/// Retrieves all AttributeData records.
		/// </summary>
		/// <returns>A list of all AttributeData records ordered by ID.</returns>
		public async Task<List<AttributeData>> GetAllAttributesData()
		{
			return await _context.AttributesData.OrderBy(a => a.Id).ToListAsync();
		}

		/// <summary>
		/// Retrieves an AttributeData by its ID.
		/// </summary>
		/// <param name="id">The ID of the AttributeData to retrieve.</param>
		/// <returns>The AttributeData with the specified ID, or null if not found.</returns>
		public async Task<AttributeData?> GetAttributeDataById(int id)
		{
			return await _context.AttributesData.Where(a => a.Id == id).FirstOrDefaultAsync();
		}

		/// <summary>
		/// Updates an existing AttributeData along with its related attributes.
		/// </summary>
		/// <param name="id">The ID of the AttributeData to update.</param>
		/// <param name="updateAttribute">Updated AttributeData properties.</param>
		/// <returns>True if the update was successful; otherwise, false.</returns>
		public async Task<bool> UpdateAttributeData(int id, UpdateAttributeDataDTO updateAttribute)
		{

			if (await AttributeDataExists(id))
				return false;

            AttributeData attributeData = await _context.AttributesData.Where(a => a.Id == id).FirstAsync();

            if (attributeData != null && updateAttribute.AttributeId != null)
                await _context.AttributesData.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.AttributeId, x => updateAttribute.AttributeId));
			if (attributeData != null && updateAttribute.Value != null)
				await _context.AttributesData.Where(r => r.Id == id)
					.ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Value, x => updateAttribute.Value));
			if (attributeData != null && updateAttribute.Key != null)
				await _context.AttributesData.Where(r => r.Id == id)
					.ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Key, x => updateAttribute.Key));
            return await _commonMethods.Save();
		}
	}
}
