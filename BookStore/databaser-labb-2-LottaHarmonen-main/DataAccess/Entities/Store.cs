using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Store
{
    public int StoreId { get; set; }

    public string? Name { get; set; }

    public string? Streetname { get; set; }

    public int? BuildingNumber { get; set; }

    public int? PostalCode { get; set; }

    public string? City { get; set; }

    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();
}
