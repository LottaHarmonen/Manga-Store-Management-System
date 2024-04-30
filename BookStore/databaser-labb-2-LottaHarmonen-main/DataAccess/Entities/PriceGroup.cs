using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class PriceGroup
{
    public int PriceGroupId { get; set; }

    public int? Price { get; set; }

    public string? Currency { get; set; }

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
}
