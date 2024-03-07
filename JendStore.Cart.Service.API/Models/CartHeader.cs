using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JendStore.Cart.Service.API.Models
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? Code { get; set; }

        [NotMapped]
        public double Discount { get; set; }

        [NotMapped]
        public double Total { get; set; }
    }
}
