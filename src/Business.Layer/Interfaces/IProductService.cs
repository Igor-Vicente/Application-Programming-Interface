using Business.Layer.Models;

namespace Business.Layer.Interfaces
{
    /*
     * Service interfaces deal with business rules, that's why we are using this interface only for the Add() and Put() and Remove() Methods, 
     * while for methods like Get(), which do not affect the database, we can directly use IRepository.
    */
    public interface IProductService : IDisposable
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid id);
    }
}
