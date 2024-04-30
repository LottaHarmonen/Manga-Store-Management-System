using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class BookModel
    {
        public long Isbn { get; set; }

        public string? Name { get; set; }

        public string? Language { get; set; }

        public virtual ICollection<BookSpecificationModel> BookSpecifications { get; set; } = new List<BookSpecificationModel>();

        public virtual ICollection<StockBalanceModel> StockBalances { get; set; } = new List<StockBalanceModel>();

        public virtual ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
    }
}
