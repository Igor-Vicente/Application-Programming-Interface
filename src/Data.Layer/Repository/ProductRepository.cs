using Business.Layer.Interfaces;
using Business.Layer.Models;
using Data.Layer.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Layer.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Product>> GetAllProductsOfSupplierAsync(Guid supplierId)
        {
            return await SearchAsync(p => p.SupplierId == supplierId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithTheirRespectiveSuppliersAsync() =>
            await Db.Products.AsNoTracking().Include(p => p.Supplier).OrderBy(p => p.Name).ToListAsync();


        public async Task<Product> GetProductWithItsRespectiveSupplierAsync(Guid productId) =>
            await Db.Products.AsNoTracking().Include(p => p.Supplier).FirstOrDefaultAsync(p => p.Id == productId);

    }
}
