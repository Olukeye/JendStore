using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using JendStore.Client.Sevice.IService;
using JendStore.Client.Utilities;

namespace JendStore.Client.Service
{
    public class ProductService: IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public async Task<ResponsDto?> GetAllProductAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponsDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = productDto,
                Url = HttpVerbs.ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponsDto?> GetProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.GET,
                Url = HttpVerbs.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponsDto?> UpdateProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.PUT,
                Url = HttpVerbs.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponsDto?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.DELETE,
                Url = HttpVerbs.ProductAPIBase + "/api/product/" + id
            });
        }
    }
}
