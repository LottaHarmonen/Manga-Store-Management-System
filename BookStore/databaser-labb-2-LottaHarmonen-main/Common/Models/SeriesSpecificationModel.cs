namespace Common.Models;

public class SeriesSpecificationModel
{
    public int SeriesId { get; set; }

    public int AuthorId { get; set; }

    public int IllustratorId { get; set; }

    public int SupplierId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? NumberOfBooks { get; set; }

    public virtual AuthorModel Author { get; set; } = null!;

    public virtual IllustratorModel Illustrator { get; set; } = null!;

    public virtual SeriesModel Series { get; set; } = null!;

    public virtual SupplierModel Supplier { get; set; } = null!;

}