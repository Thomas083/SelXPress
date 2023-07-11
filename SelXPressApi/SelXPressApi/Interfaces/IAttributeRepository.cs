using Attribute = SelXPressApi.Models.Attribute;
namespace SelXPressApi.Interfaces
{
    public interface IAttributeRepository
    {
        ICollection<Attribute> GetAllAttributes();
    }
}
