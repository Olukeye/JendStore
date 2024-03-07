using AutoMapper;
using JendStore.Cart.Service.API.DTO;
using JendStore.Cart.Service.API.Models;

namespace JendStore.Cart.Service.API.Configurations
{
    public class MapperInitilizer 
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CartDetail, CartDetailDto>().ReverseMap();
                cfg.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            });
            return config;
        }

    }
}
