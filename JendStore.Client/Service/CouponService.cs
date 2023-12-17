using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using JendStore.Client.Sevice.IServices;
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
        public async Task<ResponseDTOStatus?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDTOStatus?> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = couponDTO,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDTOStatus?> GetByCodeCouponAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/code/" + code
            });
        }

        public async Task<ResponseDTOStatus?> GetCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDTOStatus?> UpdateCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.PUT,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDTOStatus?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.DELETE,
                Url = HttpVerbs.CouponAPIBase + "/api/coupon/" + id
            });
        }
    }
}
