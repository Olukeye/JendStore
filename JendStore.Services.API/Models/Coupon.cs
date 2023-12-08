namespace JendStore.Services.API.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public int MinAmount { get; set; }
    }
}
