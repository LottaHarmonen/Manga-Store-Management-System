using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public int? MemberId { get; set; }

        public DateOnly? OrderDate { get; set; }

        public virtual MemberModel? Member { get; set; }

        public virtual ICollection<BookModel> Items { get; set; } = new List<BookModel>();
    }
}
