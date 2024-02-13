using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JendStore.Client.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO>? list = new();

            ResponsDto? response = await _couponService.GetAllCouponAsync();

            if(response != null && response.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["success"] = response?.Message;
            }
            return View(list);
        }

        public async Task<IActionResult> CreateCoupon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDTO create)
        {
            if (ModelState.IsValid)
            {
                ResponsDto? response = await _couponService.CreateCouponAsync(create);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["success"] = response?.Message;
                }
            }
            return View(create);
        }

        public async Task<IActionResult> DeleteCoupon(int couponId)
        {

            ResponsDto? response = await _couponService.GetCouponAsync(couponId);

            if(response != null && response.IsSuccess)
            {
                CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["success"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDTO couponDTO)
        {
            ResponsDto? response = await _couponService.DeleteCouponAsync(couponDTO.CouponId);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["success"] = response?.Message;
            }

            return View(couponDTO);
        }
    }  
}
