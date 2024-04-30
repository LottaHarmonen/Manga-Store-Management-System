namespace Common.Models
{

    public class StockBalanceModel
    {
        public int StoreId { get; set; }

        public long Isbn { get; set; }

        public int? Quantity { get; set; }

        public virtual BookModel IsbnNavigation { get; set; } = null!;

        public virtual StoreModel Store { get; set; } = null!;
    }

}
