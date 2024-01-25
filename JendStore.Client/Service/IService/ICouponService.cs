using JendStore.Client.Models;

namespace JendStore.Client.Service.IService
{
    public interface ICouponService
    {
        Task<ResponsDto?> GetAllCouponAsync();
        Task<ResponsDto?> GetByCodeCouponAsync(string code);
        Task<ResponsDto?> GetCouponAsync(int id);
        Task<ResponsDto?> UpdateCouponAsync(int id);
        Task<ResponsDto?> CreateCouponAsync(CouponDTO couponDTO);
        Task<ResponsDto?> DeleteCouponAsync(int id);
    }
}
