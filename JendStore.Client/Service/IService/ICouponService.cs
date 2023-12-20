﻿using JendStore.Client.Models;

namespace JendStore.Client.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDTOStatus?> GetAllCouponAsync();
        Task<ResponseDTOStatus?> GetByCodeCouponAsync(string code);
        Task<ResponseDTOStatus?> GetCouponAsync(int id);
        Task<ResponseDTOStatus?> UpdateCouponAsync(int id);
        Task<ResponseDTOStatus?> CreateCouponAsync(CouponDTO couponDTO);
        Task<ResponseDTOStatus?> DeleteCouponAsync(int id);
    }
}