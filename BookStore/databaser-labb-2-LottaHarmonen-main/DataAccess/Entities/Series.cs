using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Series
{
    public int SeriesId { get; set; }

    public string? Name { get; set; }

    public int? Demographic { get; set; }

    public int? PriceGroupId { get; set; }

    public virtual ICollection<BookSpecification> BookSpecifications { get; set; } = new List<BookSpecification>();

    public virtual Demographic? DemographicNavigation { get; set; }

    public virtual PriceGroup? PriceGroup { get; set; }

    public virtual ICollection<SeriesSpecification> SeriesSpecifications { get; set; } = new List<SeriesSpecification>();
}
