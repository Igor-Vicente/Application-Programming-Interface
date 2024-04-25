using Business.Layer.Models;

namespace Business.Layer.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsOfSupplierAsync(Guid supplierId);
        Task<IEnumerable<Product>> GetAllProductsWithTheirRespectiveSuppliersAsync();
        Task<Product> GetProductWithItsRespectiveSupplierAsync(Guid productId);
    }
}
