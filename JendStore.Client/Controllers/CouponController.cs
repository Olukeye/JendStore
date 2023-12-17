using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
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

            ResponseDTOStatus? response = await _couponService.GetAllCouponAsync();

            if(response != null && response.Status) 
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.StatusResult));
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
                ResponseDTOStatus? response = await _couponService.CreateCouponAsync(create);

                if (response != null && response.Status)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(create);
        }

        public async Task<IActionResult> DeleteCoupon(int couponId)
        {

            ResponseDTOStatus? response = await _couponService.GetCouponAsync(couponId);

            if(response != null && response.Status)
            {
                CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.StatusResult));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDTO couponDTO)
        {
               ResponseDTOStatus? response = await _couponService.DeleteCouponAsync(couponDTO.CouponId);

                if (response != null && response.Status)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }

            return View(couponDTO);
        }
    }  
}
