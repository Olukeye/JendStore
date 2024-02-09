using System.ComponentModel.DataAnnotations;

namespace JendStore.Products.Service.API.DTO
{
    public class CreateProductDto
    { 
        [Required]
        public string Name { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }
    }

    public class UpdateProduct: CreateProductDto
    {

    }

    public class ProductDto : CreateProductDto
    {
        [Key]
        public int ProductId { get; set; }
    }
}
