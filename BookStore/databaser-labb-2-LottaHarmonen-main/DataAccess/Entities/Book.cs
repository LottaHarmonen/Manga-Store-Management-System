using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Book
{
    public long Isbn { get; set; }

    public string? Name { get; set; }

    public string? Language { get; set; }

    public virtual ICollection<BookSpecification> BookSpecifications { get; set; } = new List<BookSpecification>();

    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
