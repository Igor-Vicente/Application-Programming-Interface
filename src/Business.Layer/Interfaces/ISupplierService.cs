using Business.Layer.Models;

namespace Business.Layer.Interfaces
{
    /*
     * Service interfaces deal with business rules, that's why we are using this interface only for the Add() and Put() and Remove() Methods, 
     * while for methods like Get(), which do not affect the database, we can directly use IRepository.
    */
    public interface ISupplierService : IDisposable
    {
        Task<bool> AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task RemoveAsync(Guid id);
        Task UpdateSupplierAddress(Address address);
    }
}
