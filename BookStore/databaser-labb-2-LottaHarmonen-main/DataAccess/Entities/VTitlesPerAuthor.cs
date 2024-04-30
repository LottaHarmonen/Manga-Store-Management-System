using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class VTitlesPerAuthor
{
    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    public int? Books { get; set; }

    public int? TotalInventory { get; set; }
}
