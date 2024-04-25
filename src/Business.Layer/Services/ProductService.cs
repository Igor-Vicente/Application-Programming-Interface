using Business.Layer.Interfaces;
using Business.Layer.Models;
using Business.Layer.Models.Validations;

namespace Business.Layer.Services
{
    public class ProductService : BaseService, IProductService
    {
        public readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotificator notifier)
            : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task AddAsync(Product product)
        {
            /*************** validade entity *************************/
            if (!ExecuteValidation(new ProductValidation(), product)) return;
            await _productRepository.AddAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _productRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            /*************** validade entity *************************/
            if (!ExecuteValidation(new ProductValidation(), product)) return;
            await _productRepository.UpdateAsync(product);
        }
        /*
         * The Dispose method signals that the instance (object) can be removed from memory, 
         * so when the Garbage Collector cleans it, this instance will be removed.
         */
        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
