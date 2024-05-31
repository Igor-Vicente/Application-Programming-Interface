using Asp.Versioning;
using AutoMapper;
using Business.Layer.Interfaces;
using Business.Layer.Models;
using Client.Layer.Controllers;
using Client.Layer.Dtos.Incoming;
using Client.Layer.Dtos.Outgoing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Layer.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : MainController
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(INotificator notificator,
                                IProductService productService,
                                IProductRepository productRepository,
                                IMapper mapper, IUser user) : base(notificator, user)
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
        [RequestSizeLimit(1 * 1024 * 1024)]
        [HttpPost("form-data")]
        public async Task<ActionResult> AddImage(IFormFile file)
        {
            return Ok(file);
        }

        [RequestSizeLimit(10 * 1024 * 1024)]
        [HttpPost]
        public async Task<ActionResult<OutProductDto>> AddAsync(InProductWithSupplierDto inProductWithSupplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var fileName = $"{Guid.NewGuid()}_{inProductWithSupplierDto.ImageUpload.FileName}";
            if (!await UploadFileIFormFile(inProductWithSupplierDto.ImageUpload, fileName)) return CustomResponse(ModelState);

            var product = _mapper.Map<Product>(inProductWithSupplierDto);
            product.Image = fileName;
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductWithSupplierDto UpdateProductWithSupplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var product = await _productRepository.GetByIdAsync(id);

            if (UpdateProductWithSupplierDto.ImagemUpload != null)
            {
                var fileName = $"{Guid.NewGuid()}_{UpdateProductWithSupplierDto.ImagemUpload.FileName}";
                if (!await UploadFileIFormFile(UpdateProductWithSupplierDto.ImagemUpload, fileName)) return CustomResponse(ModelState);
                product.Image = fileName;
            }

            product.Name = UpdateProductWithSupplierDto.Name;
            product.Description = UpdateProductWithSupplierDto.Description;
            product.Value = UpdateProductWithSupplierDto.Value;
            product.Active = UpdateProductWithSupplierDto.Active;

            await _productService.UpdateAsync(product);
            var outProductDto = _mapper.Map<OutProductDto>(product);
            return CustomResponse(outProductDto);
        }


        private async Task<bool> UploadFileIFormFile(IFormFile file, string fileName)
        {
            if (file == null || file.Length == 0)
            {
                NotifierError("Forneça uma imagem para este produto!");
                return false;
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

            if (System.IO.File.Exists(path))
            {
                NotifierError("Já existe arquivo com mesmo nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return true;
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
