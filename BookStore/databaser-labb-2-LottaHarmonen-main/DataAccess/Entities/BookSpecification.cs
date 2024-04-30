using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class BookSpecification
{
    public long Isbn { get; set; }

    public int AuthorId { get; set; }

    public int IllustratorId { get; set; }

    public int SeriesId { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Illustrator Illustrator { get; set; } = null!;

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Series Series { get; set; } = null!;
}
