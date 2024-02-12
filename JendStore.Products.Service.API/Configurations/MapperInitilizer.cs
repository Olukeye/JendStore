using AutoMapper;
using JendStore.Products.Service.API.DTO;
using JendStore.Products.Service.API.Models;

namespace JendStore.Products.Service.API.Configurations
{
    public class MapperInitilizer 
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
                cfg.CreateMap<Product, CreateProductDto>().ReverseMap();
            });
            return config;
        }

    }
}
