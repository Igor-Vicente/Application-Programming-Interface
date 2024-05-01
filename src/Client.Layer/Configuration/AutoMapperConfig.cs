using AutoMapper;
using Business.Layer.Models;
using Client.Layer.Dtos.Incoming;
using Client.Layer.Dtos.Outgoing;

namespace Client.Layer.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<InSupplierDto, Supplier>();
            CreateMap<InProductDto, Product>();
            CreateMap<InAddressDto, Address>();
            CreateMap<Supplier, OutSupplierDto>().ReverseMap();
            CreateMap<Product, OutProductDto>().ReverseMap();
            CreateMap<Address, OutAddressDto>().ReverseMap();

            CreateMap<Product, OutProductWithSupplierDto>().ReverseMap();
            CreateMap<InProductWithSupplierDto, Product>();
        }
    }
}
