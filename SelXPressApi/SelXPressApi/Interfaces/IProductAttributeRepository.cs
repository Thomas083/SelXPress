using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IProductAttributeRepository
    {
        ICollection<ProductAttribute> GetAllProductAttributes();
    }
}
