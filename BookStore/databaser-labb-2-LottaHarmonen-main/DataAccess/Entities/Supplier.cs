using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? Name { get; set; }

    public long? ContactNumber { get; set; }

    public virtual ICollection<SeriesSpecification> SeriesSpecifications { get; set; } = new List<SeriesSpecification>();
}
