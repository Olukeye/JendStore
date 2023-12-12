using JendStore.Client.Iservices;
using JendStore.Client.Models;
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
    }
}
