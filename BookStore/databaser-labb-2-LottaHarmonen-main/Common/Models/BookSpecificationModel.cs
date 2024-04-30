using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class BookSpecificationModel
    {
        public long Isbn { get; set; }

        public int AuthorId { get; set; }

        public int IllustratorId { get; set; }

        public int SeriesId { get; set; }

        public DateOnly? PublicationDate { get; set; }

        public virtual AuthorModel Author { get; set; } = null!;

        public virtual IllustratorModel Illustrator { get; set; } = null!;

        public virtual BookModel IsbnNavigation { get; set; } = null!;

        public virtual SeriesModel Series { get; set; } = null!;
    }
}
