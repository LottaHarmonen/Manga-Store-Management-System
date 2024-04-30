using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class StockBalance
{
    public int StoreId { get; set; }

    public long Isbn { get; set; }

    public int? Quantity { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
