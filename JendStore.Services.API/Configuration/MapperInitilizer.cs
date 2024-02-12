using AutoMapper;
using JendStore.Services.API.DTO;
using JendStore.Services.API.Models;

namespace JendStore.Services.API.Configuration
{
    public class MapperInitilizer
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Coupon, CouponDTO>().ReverseMap();
                cfg.CreateMap<Coupon, CreateCouponDTO>().ReverseMap();
            });
            return config;
        }

    }
}
