using System.ComponentModel.DataAnnotations;

namespace JendStore.Cart.Service.API.DTO
{
   
    public class CreateCouponDTO
    {
        public string Code { get; set; }
        public double Discount { get; set; }
        public int MinAmount { get; set; }
    }

    public class UpdateCouponDTO: CreateCouponDTO
    {

    }

    public class CouponDTO : CreateCouponDTO
    {
        public int CouponId { get; set; }
    }
}
