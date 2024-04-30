using Common.Models;
using DataAccess;
using DataAccess.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Common.Services;

public class StoreRepository
{
    private readonly BookStoreContext _dbContext;

    public StoreRepository(BookStoreContext dbContext)
    {
        _dbContext = dbContext;
    }


    public StoreModel GetById(int isbn)
    {

        var storeTemporaryFind = _dbContext.Stores.Find(isbn);
        var foundStore = new StoreModel
        {
            StoreId = storeTemporaryFind.StoreId,
            Name = storeTemporaryFind.Name,
            BuildingNumber = storeTemporaryFind.BuildingNumber,
            City = storeTemporaryFind.City,
            PostalCode = storeTemporaryFind.PostalCode,
            Streetname = storeTemporaryFind.Streetname,
        };

        return foundStore;
    }


    public StoreModel GetByName(string storeName)
    {
        var selectedStore = _dbContext.Stores.FirstOrDefault(s => s.Name == storeName);

        

        StoreModel selectedStoreModel = new StoreModel()
        {
            Name = selectedStore.Name,
            StoreId = selectedStore.StoreId,
            BuildingNumber = selectedStore.BuildingNumber,
            City = selectedStore.City,
            PostalCode = selectedStore.PostalCode,
            Streetname = selectedStore.Streetname,
        };


        return selectedStoreModel;
    }

    public IEnumerable<StoreModel> GetAll()
    {
        return _dbContext.Stores.Select
        (
            Store => new StoreModel()
            {
                StoreId = Store.StoreId,
                Name = Store.Name,
                Streetname = Store.Streetname,
                BuildingNumber = Store.BuildingNumber,
                PostalCode = Store.PostalCode,
                City = Store.City,
            }
        ).ToList();

    }




    public void Add(StoreModel entity)
    {
        throw new NotImplementedException();
    }

    public void Update(StoreModel entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    
}
