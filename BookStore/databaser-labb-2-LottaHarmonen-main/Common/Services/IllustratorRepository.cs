using Common.Models;
using DataAccess;
using DataAccess.Entities;

namespace Common.Services;

public class IllustratorRepository
{
    private readonly BookStoreContext _dbContext;

    public IllustratorRepository(BookStoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<IllustratorModel> GetById(long isbn)
    {
        var illustratorIds = _dbContext.BookSpecifications
            .Where(a => a.Isbn == isbn)
            .Select(a => a.IllustratorId)
            .ToList();

        var illustrators = _dbContext.Illustrators
            .Where(a => illustratorIds.Contains(a.IllustratorId))
            .Select(a => new IllustratorModel()
            {
                DateOfBirth = a.DateOfBirth,
                Firstname = a.Firstname,
                IllustratorId = a.IllustratorId,
                Lastname = a.Lastname,

            })
            .ToList();

        return illustrators;
    }

    public IEnumerable<IllustratorModel> GetAll()
    {
        return _dbContext.Illustrators.Select
        (
            Illustrator => new IllustratorModel()
            {
                IllustratorId = Illustrator.IllustratorId,
                Firstname = Illustrator.Firstname,
                Lastname = Illustrator.Lastname,
                DateOfBirth = Illustrator.DateOfBirth,
            }
        ).ToList();
    }

    public void Add(IllustratorModel entity)
    {
        throw new NotImplementedException();
    }

    public void Update(IllustratorModel entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IllustratorModel GetByName(string boxIllustratorText)
    {
        var illustrator = _dbContext.Illustrators.FirstOrDefault(a => a.Firstname == boxIllustratorText);

        var newIllustratorModel = new IllustratorModel()
        {
            IllustratorId = illustrator.IllustratorId,
            Firstname = illustrator.Firstname,
            Lastname = illustrator.Lastname,
            DateOfBirth = illustrator.DateOfBirth,
        };

        return newIllustratorModel;

    }
}