using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Illustrator
{
    public int IllustratorId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public virtual ICollection<BookSpecification> BookSpecifications { get; set; } = new List<BookSpecification>();

    public virtual ICollection<SeriesSpecification> SeriesSpecifications { get; set; } = new List<SeriesSpecification>();
}
