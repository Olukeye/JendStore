using System.ComponentModel.DataAnnotations;

namespace JendStore.Services.API.DTO
{
   
    public class CreateCouponDTO
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public double Discount { get; set; }

        public int MinAmount { get; set; }
    }

    public class UpdateCouponDTO: CreateCouponDTO
    {

    }

    public class CouponDTO : CreateCouponDTO
    {
        [Key]
        public int CouponId { get; set; }
    }
}
