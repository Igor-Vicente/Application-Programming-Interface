using Business.Layer.Interfaces;
using Business.Layer.Models;
using Business.Layer.Models.Validations;

namespace Business.Layer.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository, INotificator notifier)
            : base(notifier)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<bool> AddAsync(Supplier supplier)
        {
            /*************** validade entity *************************/
            if (!ExecuteValidation(new SupplierValidation(), supplier) || !ExecuteValidation(new AddressValidation(), supplier.Address)) return false;
            /*************** validade duplicity **********************/
            if (_supplierRepository.SearchAsync(s => s.Document == supplier.Document).Result.Any())
            {
                Notifier("The informed document is already being used.");
                return false;
            }

            await _supplierRepository.AddAsync(supplier);
            return true;
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            /*************** validade entity *************************/
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;
            /*************** validade duplicity **********************/
            if (_supplierRepository.SearchAsync(s => s.Document == supplier.Document && s.Id != supplier.Id).Result.Any())
            {
                Notifier("The informed document is already being used.");
                return;
            }

            await _supplierRepository.UpdateAsync(supplier);
        }
        public async Task RemoveAsync(Guid id)
        {
            var supplier = await _supplierRepository.GetSupplierWithItsRespectiveProductsAndAddressAsync(id);
            if (supplier == null)
            {
                Notifier("Supplier doesn't exist.");
                return;
            }
            if (supplier.Products.Any())
            {
                Notifier("The supplier has registered products.");
                return;
            }
            /*************** todo: verificar necessidade de fazer essa consulta, visto que var supplier obtém endereco *******************/
            var address = await _supplierRepository.GetAddressOfTheSupplierAsync(supplier.Id);
            if (address != null)
            {
                await _supplierRepository.RemoveAddressOfTheSupplierAsync(address);
            }

            await _supplierRepository.RemoveAsync(id);
        }
        public async Task UpdateSupplierAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _supplierRepository.UpdateAddressOfTheSupplier(address);
        }

        /*
         * The Dispose method signals that the instance (object) can be removed from memory, 
         * so when the Garbage Collector cleans it, this instance will be removed.
         */
        public void Dispose()
        {
            _supplierRepository?.Dispose();
        }
    }
}
