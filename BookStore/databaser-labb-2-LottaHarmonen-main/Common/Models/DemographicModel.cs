using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DemographicModel
    {
        public int DemographicId { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<SeriesModel> Series { get; set; } = new List<SeriesModel>();
    }
}
