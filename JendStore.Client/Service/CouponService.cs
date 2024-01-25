using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using JendStore.Client.Sevice.IService;
using JendStore.Client.Utilities;


namespace JendStore.Client.Sevice
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public async Task<ResponsDto?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponsDto?> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = couponDTO,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponsDto?> GetByCodeCouponAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/code/" + code
            });
        }

        public async Task<ResponsDto?> GetCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponsDto?> UpdateCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.PUT,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponsDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.DELETE,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/" + id
            });
        }
    }
}
