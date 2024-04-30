using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class VSoldPerBook
{
    public string? NameOfTheBook { get; set; }

    public int? AmountSold { get; set; }

    public string? Language { get; set; }
}
