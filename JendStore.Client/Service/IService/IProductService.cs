using JendStore.Client.Models;

namespace JendStore.Client.Service.IService
{
    public interface IProductService
    {
        Task<ResponsDto?> GetAllProductAsync();
        Task<ResponsDto?> GetProductAsync(int id);
        Task<ResponsDto?> UpdateProductAsync(int id);
        Task<ResponsDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponsDto?> DeleteProductAsync(int id);
    }
}
