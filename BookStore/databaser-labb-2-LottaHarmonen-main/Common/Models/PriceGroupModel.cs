using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class PriceGroupModel
    {
        public int PriceGroupId { get; set; }

        public int? Price { get; set; }

        public string? Currency { get; set; }

        public virtual ICollection<SeriesModel> Series { get; set; } = new List<SeriesModel>();
    }
}
