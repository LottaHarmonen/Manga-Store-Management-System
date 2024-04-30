using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? MemberId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<Book> Items { get; set; } = new List<Book>();
}
