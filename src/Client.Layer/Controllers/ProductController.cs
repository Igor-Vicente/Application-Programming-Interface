using AutoMapper;
using Business.Layer.Interfaces;
using Business.Layer.Models;
using Client.Layer.Dtos.Incoming;
using Client.Layer.Dtos.Outgoing;
using Microsoft.AspNetCore.Mvc;

namespace Client.Layer.Controllers
{
    [Route("api/products")]
    public class ProductController : MainController
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(INotificator notificator,
                                IProductService productService,
                                IProductRepository productRepository,
                                IMapper mapper) : base(notificator)
        {
            _productService = productService;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<OutProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var outProductsDto = _mapper.Map<IEnumerable<OutProductDto>>(products);
            return outProductsDto;
        }

        [HttpGet("{id:guid}")]
        public async Task<OutProductWithSupplierDto> GetProductById(Guid id)
        {
            var product = await _productRepository.GetProductWithItsRespectiveSupplierAsync(id);

            var outProductWithSupplierDto = _mapper.Map<OutProductWithSupplierDto>(product);
            return outProductWithSupplierDto;
        }

        [HttpPost]
        public async Task<ActionResult<OutProductDto>> AddAsync(InProductWithSupplierDto inProductWithSupplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var product = _mapper.Map<Product>(inProductWithSupplierDto);

            await _productService.AddAsync(product);

            var outProductDto = _mapper.Map<OutProductDto>(product);
            return CustomResponse(outProductDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<OutProductDto>> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.RemoveAsync(id);

            var outProductDto = _mapper.Map<OutProductDto>(product);
            return CustomResponse(outProductDto);
        }


        private bool UploadFileBase64(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotifierError("Forneça uma imagem para este produto!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                NotifierError("Já existe um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
    }
}
