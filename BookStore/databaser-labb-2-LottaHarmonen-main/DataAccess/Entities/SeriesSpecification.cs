using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class SeriesSpecification
{
    public int SeriesId { get; set; }

    public int AuthorId { get; set; }

    public int IllustratorId { get; set; }

    public int SupplierId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? NumberOfBooks { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Illustrator Illustrator { get; set; } = null!;

    public virtual Series Series { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
