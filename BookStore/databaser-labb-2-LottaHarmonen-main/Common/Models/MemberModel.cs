using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class MemberModel
    {
        public int MemberId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public virtual ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
    }
}
