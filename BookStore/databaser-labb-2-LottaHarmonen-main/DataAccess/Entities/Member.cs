using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Member
{
    public int MemberId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
