using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly DataContext _context;

        public StockRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Stock> GetAllStocks()
        {
            return _context.Stocks.OrderBy(s => s.Id).ToList();
        }
    }
}
