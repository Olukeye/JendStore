using AutoMapper;
using JendStore.Products.Service.API.DTO;
using JendStore.Products.Service.API.Models;

namespace JendStore.Products.Service.API.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
        }

    }
}
