using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class SeriesModel
    {
        public int SeriesId { get; set; }

        public string? Name { get; set; }

        public int? Demographic { get; set; }

        public int? PriceGroupId { get; set; }

        public virtual ICollection<BookSpecificationModel> BookSpecifications { get; set; } = new List<BookSpecificationModel>();

        public virtual DemographicModel? DemographicNavigation { get; set; }

        public virtual PriceGroupModel? PriceGroup { get; set; }

        public virtual ICollection<SeriesSpecificationModel> SeriesSpecifications { get; set; } = new List<SeriesSpecificationModel>();
    }
}
