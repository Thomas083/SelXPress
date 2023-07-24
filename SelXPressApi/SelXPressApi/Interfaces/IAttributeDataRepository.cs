using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IAttributeDataRepository
    {
        Task<List<AttributeData>> GetAllAttributesData();
        Task<AttributeData?> GetAttributeDataById(int id);
        Task<bool> AttributeDataExists(int id);
        Task<bool> CreateAttributeData(CreateAttributeDataDTO createAttribute);
        Task<bool> UpdateAttributeData(int id, UpdateAttributeDataDTO updateAttribute);
        Task<bool> DeleteAttributeData(int id);
    }
}
