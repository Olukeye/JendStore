using AutoMapper;
using JendStore.Services.API.DTO;
using JendStore.Services.API.Models;

namespace JendStore.Services.API.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Coupon, CouponDTO>().ReverseMap();
            CreateMap<Coupon, CreateCouponDTO>().ReverseMap();
            CreateMap<Coupon, UpdateCouponDTO>().ReverseMap();
        }

    }
}
