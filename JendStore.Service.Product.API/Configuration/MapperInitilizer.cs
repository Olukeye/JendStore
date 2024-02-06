using AutoMapper;
using JendStore.Service.Product.API.DTO;
using JendStore.Service.Product.API.Models;

namespace JendStore.Service.Product.API.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Products, ProductDto>().ReverseMap();
            //CreateMap<Coupon, CreateCouponDTO>().ReverseMap();
            //CreateMap<Coupon, UpdateCouponDTO>().ReverseMap();
        }
    }
}
