using SelXPressApi.DTO.AttributeDTO;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Interfaces
{
    public interface IAttributeRepository
    {
        Task<List<Attribute>> GetAllAttributes();
        Task<Attribute?> GetAttributeById(int id);
        Task<bool> AttributeExists(int id);
        Task<bool> CreateAttribute(CreateAttributeDTO createAttribute);
        Task<bool> UpdateAttribute(UpdateAttributeDTO updateAttribute);
        Task<bool> DeleteAttribute(int id);
    }
}
