using Business.Layer.Models;

namespace Business.Layer.Interfaces
{

    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierWithItsRespectiveAddressAsync(Guid supplierId);
        Task<Supplier> GetSupplierWithItsRespectiveProductsAndAddressAsync(Guid supplierId);
        /*
         * According to DDD "Aggregates bring together one or more entities into a single abstraction"
         * Therefore, a single Repository is necessary to deal with Supplier and its Address aggregate.
         * (Assim, um único Repository se faz necessário para lidar com Supplier e o seu agregado Address.)
        */
        Task<Address> GetAddressOfTheSupplierAsync(Guid supplierId);
        Task RemoveAddressOfTheSupplierAsync(Address address);
    }
}
