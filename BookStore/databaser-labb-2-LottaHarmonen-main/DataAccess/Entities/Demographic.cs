using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Demographic
{
    public int DemographicId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
}
