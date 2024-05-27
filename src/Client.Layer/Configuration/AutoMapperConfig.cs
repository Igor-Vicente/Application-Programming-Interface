using AutoMapper;
using Business.Layer.Models;
using Client.Layer.Dtos.Incoming;
using Client.Layer.Dtos.Outgoing;

namespace Client.Layer.Configuration
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<InSupplierDto, Supplier>();
            CreateMap<InProductDto, Product>();
            CreateMap<InAddressDto, Address>();
            CreateMap<Supplier, OutSupplierDto>().ReverseMap();
            CreateMap<Product, OutProductDto>().ReverseMap();
            CreateMap<Address, OutAddressDto>().ReverseMap();

            CreateMap<Product, OutProductWithSupplierDto>().ReverseMap();
            CreateMap<InProductWithSupplierDto, Product>();
            CreateMap<UpdateProductWithSupplierDto, Product>();
        }
    }
}
