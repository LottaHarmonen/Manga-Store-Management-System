using System.ComponentModel;
using System.Globalization;
using Common.Models;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Common.Services;

public class BookRepository
{
    private readonly BookStoreContext _dbContext;

    public BookRepository(BookStoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public event Action BookListChanged;
    public event Action StoreBookListChanged;

    public IEnumerable<BookModel> GetAll()
    {

        return _dbContext.Books.Select
        (
            book => new BookModel()
            {
                Isbn = book.Isbn,
                Name = book.Name,

            }
        ).ToList();
    }

    public BookModel getById(long ISBN)
    {
        var book = _dbContext.Books.FirstOrDefault(b => b.Isbn == ISBN);

        BookModel chosenBookModel = new BookModel()
        {
            Isbn = book.Isbn,
            Name = book.Name,
            Language = book.Language,
        };

        return chosenBookModel;
    }


    public bool IfISBNExists(long isbn)
    {
        // Assuming your repository has a method to get all books or a similar mechanism
        var allBooks = GetAll();

        // Check if any book has the same ISBN
        return allBooks.Any(book => book.Isbn == isbn);
    }

    public BookModel? GetByName(object bookName)
    {
        Book book1 = _dbContext.Books.SingleOrDefault(b => b.Name == bookName);

        if (book1 == null)
        {
            BookListChanged.Invoke();
            return null;
        }
        else
        {
            BookModel newBook = new BookModel
            {
                Isbn = book1.Isbn,
                Name = book1.Name,
                Language = book1.Language,
            };
            return newBook;

        }
    }

    public BookSpecificationModel GetDate(long isbn)
    {
        var bookSpec = _dbContext.BookSpecifications.FirstOrDefault(a => a.Isbn == isbn);

        return new BookSpecificationModel
        {
            Isbn = bookSpec.Isbn,
            AuthorId = bookSpec.AuthorId,
            IllustratorId = bookSpec.IllustratorId,
            SeriesId = bookSpec.SeriesId,
            PublicationDate = bookSpec.PublicationDate,
        };
    }


    public void Add(BookModel newBook)
    {
        var book = new Book()
        {
            Isbn = newBook.Isbn,
            Name = newBook.Name,
            Language = newBook.Language,
        };

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();

        BookListChanged.Invoke();
    }


    public void AddSpec(BookSpecificationModel bookSpec)
    {
        var bookspecification = new BookSpecification()
        {
            AuthorId = bookSpec.AuthorId,
            Isbn = bookSpec.Isbn,
            IllustratorId = bookSpec.IllustratorId,
            SeriesId = bookSpec.SeriesId,
            PublicationDate = bookSpec.PublicationDate,

        };
        _dbContext.BookSpecifications.Add(bookspecification);
        _dbContext.SaveChanges();
        BookListChanged.Invoke();
    }

    public void Update(BookModel newBook, BookSpecificationModel bookSpec)
    {
        var bookToUpdate = _dbContext.Books.SingleOrDefault(b => b.Isbn == newBook.Isbn);

        bookToUpdate.Isbn = newBook.Isbn;
        bookToUpdate.Name = newBook.Name;
        bookToUpdate.Language = newBook.Language;

        var specToUpdate = _dbContext.BookSpecifications.Single(s => s.Isbn == newBook.Isbn);
        specToUpdate.AuthorId = bookSpec.AuthorId;
        specToUpdate.Isbn = bookSpec.Isbn;
        specToUpdate.IllustratorId = bookSpec.IllustratorId;
        specToUpdate.SeriesId = bookSpec.SeriesId;
        specToUpdate.PublicationDate = bookSpec.PublicationDate;

        _dbContext.SaveChanges();
        BookListChanged.Invoke();

    }

    public Dictionary<string, int> GetAllByStore(StoreModel store)
    {
        var allBooksInChosenStore = _dbContext.StockBalances
            .Where(b => b.StoreId == store.StoreId)
            .ToList();

        var allBooks = GetAll();

        Dictionary<string, int> booksInStore = new Dictionary<string, int>();

        foreach (var stockBalance in allBooksInChosenStore)
        {
            var book = allBooks.FirstOrDefault(b => b.Isbn == stockBalance.Isbn);

            booksInStore[book.Name] = (int)stockBalance.Quantity;
        }

        return booksInStore;

    }

    public void TransferBooks(object selectedBook, object comboboxstore)
    {
        StockBalanceRepository stockRepo = new StockBalanceRepository(_dbContext);

        StoreModel selectedStore = (StoreModel)comboboxstore;

        var selectedBookModel = GetByName(selectedBook);

        var allStocks = stockRepo.GetAllStocks();

        if (allStocks.Any(s => s.Isbn == selectedBookModel.Isbn && s.StoreId == selectedStore.StoreId))
        {

            var stockToUpdate = _dbContext.StockBalances.First(s => s.Isbn == selectedBookModel.Isbn && s.StoreId == selectedStore.StoreId);
            stockToUpdate.Quantity += 1;

        }
        else
        {
            StockBalance newStock = new StockBalance
            {
                Isbn = selectedBookModel.Isbn,
                Quantity = 1,
                StoreId = selectedStore.StoreId,

            };
            _dbContext.StockBalances.Add(newStock);
        }

        _dbContext.SaveChanges();
        StoreBookListChanged.Invoke();
    }

    public void DeleteBooksFromStore(object selectedObject, object comboboxstore)
    {
        StockBalanceRepository stockRepo = new StockBalanceRepository(_dbContext);

        
        string title = (string)selectedObject.GetType().GetProperty("Title").GetValue(selectedObject);
        int quantity = (int)selectedObject.GetType().GetProperty("Quantity").GetValue(selectedObject);

        StoreModel selectedStore = (StoreModel)comboboxstore;
        var selectedBook = _dbContext.Books.FirstOrDefault(b => b.Name == title);

        var toBeRemoved = _dbContext.StockBalances.FirstOrDefault(s => s.Isbn == selectedBook.Isbn && s.StoreId == selectedStore.StoreId);
        toBeRemoved.Quantity -= 1;
        _dbContext.SaveChanges();

        if (_dbContext.StockBalances.Any(q => q.Quantity == 0))
        {
            _dbContext.StockBalances.Remove(toBeRemoved);
        }

        _dbContext.SaveChanges();
        StoreBookListChanged.Invoke();
        
    }
}
