using Business.Layer.Interfaces;
using Business.Layer.Models;
using Data.Layer.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Layer.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context) { }


        public async Task<Supplier> GetSupplierWithItsRespectiveAddressAsync(Guid supplierId) =>
            await Db.Suppliers.AsNoTracking().Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == supplierId);

        public async Task<Supplier> GetSupplierWithItsRespectiveProductsAndAddressAsync(Guid supplierId) =>
            await Db.Suppliers.AsNoTracking().Include(s => s.Products).Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == supplierId);

        public async Task<Address> GetAddressOfTheSupplierAsync(Guid supplierId) =>
            await Db.Adresses.AsNoTracking().FirstOrDefaultAsync(a => a.SupplierId == supplierId);

        public async Task<Address> GetAddressByIdAsync(Guid addressId) =>
            await Db.Adresses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == addressId);

        public async Task UpdateAddressOfTheSupplier(Address address)
        {
            Db.Adresses.Update(address);
            await SaveChangesAsync();
        }

        public async Task RemoveAddressOfTheSupplierAsync(Address address)
        {
            Db.Adresses.Remove(address);
            await SaveChangesAsync();
        }
    }
}
