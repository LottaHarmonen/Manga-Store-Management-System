using Common.Models;
using DataAccess;

namespace Common.Services;

public class StockBalanceRepository
{
    private readonly BookStoreContext _dbContext;

    public StockBalanceRepository(BookStoreContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IEnumerable<StockBalanceModel> GetAllStocks()
    {
        return _dbContext.StockBalances.Select
        (
            stock => new StockBalanceModel()
            {
                Isbn = stock.Isbn,
                StoreId = stock.StoreId,
                Quantity = stock.Quantity,
            }

        ).ToList();
    }

}