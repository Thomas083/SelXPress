using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IStockRepository
    {
        ICollection<Stock> GetAllStocks();
    }
}
