namespace Common.Models;

public class StoreModel
{
    public int StoreId { get; set; }

    public string? Name { get; set; }

    public string? Streetname { get; set; }

    public int? BuildingNumber { get; set; }

    public int? PostalCode { get; set; }

    public string? City { get; set; }

    public virtual ICollection<StockBalanceModel> StockBalances { get; set; } = new List<StockBalanceModel>();

}