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

        public Task<bool> AttributeDataExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAttributeData(CreateAttributeDataDTO createAttribute)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAttributeData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AttributeData>> GetAllAttributesData()
        {
            throw new NotImplementedException();
        }

        public Task<AttributeData?> GetAttributeDataById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAttributeData(UpdateAttributeDataDTO updateAttribute)
        {
            throw new NotImplementedException();
        }
    }
}
