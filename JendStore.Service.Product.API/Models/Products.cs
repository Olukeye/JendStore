using System.ComponentModel.DataAnnotations;

namespace JendStore.Service.Product.API.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }
    }
}
