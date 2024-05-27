using AutoMapper;
using Business.Layer.Interfaces;
using Business.Layer.Models;
using Client.Layer.Dtos.Incoming;
using Client.Layer.Dtos.Outgoing;
using Client.Layer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Layer.Controllers
{
    [Authorize]
    [Route("api/suppliers")]
    public class SupplierController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierRepository supplierRepository,
            IMapper mapper, ISupplierService supplierService,
            INotificator notificador, IUser user) : base(notificador, user)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _supplierService = supplierService;
        }

        [ClaimsAuthorize("Supplier", "Read")]
        [HttpGet]
        public async Task<IEnumerable<OutSupplierDto>> GetAllAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            var outSuppliersDto = _mapper.Map<IEnumerable<OutSupplierDto>>(suppliers);

            return outSuppliersDto;
        }

        [ClaimsAuthorize("Supplier", "Read")]
        [HttpGet("{id:Guid}")]
        public async Task<OutSupplierDto> GetByIdAsync(Guid id)
        {
            var supplier = await _supplierRepository.GetSupplierWithItsRespectiveProductsAndAddressAsync(id);
            var outSupplierDto = _mapper.Map<OutSupplierDto>(supplier);

            return outSupplierDto;
        }

        [ClaimsAuthorize("Supplier", "Create")]
        [HttpPost]
        public async Task<ActionResult<OutSupplierDto>> AddAsync(InSupplierDto inSupplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var supplier = _mapper.Map<Supplier>(inSupplierDto);
            await _supplierService.AddAsync(supplier);

            var outSupplier = _mapper.Map<OutSupplierDto>(supplier);
            return CustomResponse(outSupplier);
        }

        [ClaimsAuthorize("Supplier", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<OutSupplierDto>> UpdateAsync(Guid id, OutSupplierDto outSupplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var supplier = await _supplierRepository.GetSupplierWithItsRespectiveProductsAndAddressAsync(id);
            if (supplier == null)
            {
                NotifierError("This identifier does not belong to the supplier.");
                return CustomResponse(outSupplierDto);
            }

            supplier = _mapper.Map<Supplier>(outSupplierDto);
            await _supplierService.UpdateAsync(supplier);

            return CustomResponse(outSupplierDto);
        }

        [ClaimsAuthorize("Supplier", "Delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<OutSupplierDto>> DeleteAsync(Guid id)
        {
            var supplier = await _supplierRepository.GetSupplierWithItsRespectiveAddressAsync(id);
            if (supplier == null) NotFound();
            await _supplierService.RemoveAsync(id);

            var outSupplierDto = _mapper.Map<OutSupplierDto>(supplier);
            return CustomResponse(outSupplierDto);
        }

        [ClaimsAuthorize("Supplier", "Read")]
        [HttpGet("address/{id:guid}")]
        public async Task<OutAddressDto> GetAddressByIdAsync(Guid id)
        {
            var address = await _supplierRepository.GetAddressByIdAsync(id);
            if (address == null) NotFound();
            var outAddressDto = _mapper.Map<OutAddressDto>(address);
            return outAddressDto;
        }
    }
}
