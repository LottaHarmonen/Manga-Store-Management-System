using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class AuthorModel
    {

        public int AuthorId { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string FullName => $"{Firstname} {Lastname}";

        public DateOnly? DateOfBirth { get; set; }

        public virtual ICollection<BookSpecificationModel> BookSpecifications { get; set; } = new List<BookSpecificationModel>();

        public virtual ICollection<SeriesSpecificationModel> SeriesSpecifications { get; set; } = new List<SeriesSpecificationModel>();
    }
}
