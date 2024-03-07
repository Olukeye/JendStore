using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JendStore.Cart.Service.API.DTO;

namespace JendStore.Cart.Service.API.Models
{
    public class CartDetail
    {
        [Key]
        public int CartDetailId { get; set; }
        public int CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }

        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
