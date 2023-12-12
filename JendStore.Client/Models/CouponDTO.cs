namespace JendStore.Client.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public int MinAmount { get; set; }
    }
}
