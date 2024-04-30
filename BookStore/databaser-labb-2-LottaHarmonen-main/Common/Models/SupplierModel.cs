namespace Common.Models;

public class SupplierModel
{
    public int SupplierId { get; set; }

    public string? Name { get; set; }

    public long? ContactNumber { get; set; }

    public virtual ICollection<SeriesSpecificationModel> SeriesSpecifications { get; set; } = new List<SeriesSpecificationModel>();

}