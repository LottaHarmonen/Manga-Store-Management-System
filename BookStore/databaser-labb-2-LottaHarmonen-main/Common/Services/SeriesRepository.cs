using Common.Models;
using DataAccess;

namespace Common.Services;

public class SeriesRepository
{
    private readonly BookStoreContext _dbContext;

    public SeriesRepository(BookStoreContext dbContext)
    {
        _dbContext = dbContext;
    }


    public SeriesModel GetById(long isbn)
    {
        var specification = _dbContext.BookSpecifications.FirstOrDefault(a => a.Isbn == isbn);

        var series = _dbContext.Series.FirstOrDefault(a => a.SeriesId == specification.SeriesId);

        var newSeriesModel = new SeriesModel()
        {
            Name = series.Name,
            SeriesId = series.SeriesId,
            Demographic = series.Demographic,
            PriceGroupId = series.PriceGroupId,
        };

        return newSeriesModel;
    }

    public IEnumerable<SeriesModel> GetAll()
    {
        return _dbContext.Series.Select
        (
            Series => new SeriesModel()
            {
                SeriesId = Series.SeriesId,
                Demographic = Series.Demographic,
                Name = Series.Name,
                PriceGroupId = Series.PriceGroupId,

            }
        ).ToList();
    }

    public SeriesModel GetByName(string boxSeriesText)
    {
        var series = _dbContext.Series.FirstOrDefault(a => a.Name == boxSeriesText);

        var newSeriesModel = new SeriesModel()
        {
            SeriesId = series.SeriesId,
            Name = series.Name,
            Demographic = series.Demographic,
            PriceGroupId = series.PriceGroupId,
            
        };

        return newSeriesModel;
    }
}