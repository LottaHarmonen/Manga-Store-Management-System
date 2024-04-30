using Common.Models;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Common.Services;

public class AuthorRepository
{
    private readonly BookStoreContext _dbContext;

    public AuthorRepository(BookStoreContext dbContext)
    {
        _dbContext = dbContext;
        
    }

    public event Action AuthorListChanged;

    public IEnumerable<AuthorModel> GetById(long isbn)
    {
        var authorIds = _dbContext.BookSpecifications
            .Where(a => a.Isbn == isbn)
            .Select(a => a.AuthorId)
            .ToList();

        var authors = _dbContext.Authors
            .Where(a => authorIds.Contains(a.AuthorId))
            .Select(a => new AuthorModel
            {
                AuthorId = a.AuthorId,
                Firstname = a.Firstname,
                Lastname = a.Lastname,
                DateOfBirth = a.DateOfBirth,

            })
            .ToList();

        return authors;

    }

    public IEnumerable<AuthorModel> GetAll()
    {
        return _dbContext.Authors.Select
        (
            Author => new AuthorModel()
            {
                AuthorId = Author.AuthorId,
                Firstname = Author.Firstname,
                Lastname = Author.Lastname,
                DateOfBirth = Author.DateOfBirth
            }
        ).ToList();
    }


    public AuthorModel GetByName(string firstname)
    {

        //hittar inget
        var author = _dbContext.Authors.FirstOrDefault(a => a.Firstname == firstname);

        var newauthor = new AuthorModel
        {
            AuthorId = author.AuthorId,
            Firstname = author.Firstname,
            Lastname = author.Lastname,
            DateOfBirth = author.DateOfBirth,
        };

        return newauthor;
    }


    public void Add(AuthorModel newAuthorModel)
    {

        if (_dbContext.Authors.Any(a => a.Firstname == newAuthorModel.Firstname && 
                                              a.Lastname == newAuthorModel.Lastname && 
                                              a.DateOfBirth == newAuthorModel.DateOfBirth))
        {
            return;
        }
        else
        {
            int highestId = _dbContext.Authors.Max(a => a.AuthorId);
            int newAuthorId = highestId + 1;

            var newAuthor = new Author
            {
                AuthorId = newAuthorId,
                Firstname = newAuthorModel.Firstname,
                Lastname = newAuthorModel.Lastname,
                DateOfBirth = newAuthorModel.DateOfBirth
            };

            _dbContext.Authors.Add(newAuthor);
            _dbContext.SaveChanges();

            AuthorListChanged.Invoke();
        }

    }

    public void Update(AuthorModel newAuthorInformation, AuthorModel authorModel)
    {

        var author = _dbContext.Authors.Find(authorModel.AuthorId);

        author.DateOfBirth = newAuthorInformation.DateOfBirth;
        author.Firstname = newAuthorInformation.Firstname;
        author.Lastname = newAuthorInformation.Lastname;

        _dbContext.SaveChanges();

        AuthorListChanged.Invoke();

    }
}