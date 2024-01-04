using JendStore.Client.Models;
using JendStore.Client.Sevice.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static JendStore.Client.Utilities.HttpVerbs;

namespace JendStore.Client.Sevice
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTOStatus?> SendAsync(RequestDTOModel requestDTOModel)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("JendStoreAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //

                message.RequestUri = new Uri(requestDTOModel.Url);
                if (requestDTOModel != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTOModel.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDTOModel.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound: return new() { Status = false, Message = "Not Found" };
                    case HttpStatusCode.Unauthorized: return new() { Status = false, Message = "UnAuthorized" };
                    case HttpStatusCode.Forbidden: return new() { Status = false, Message = "Permission Denied" };
                    case HttpStatusCode.BadRequest: return new() { Status = false, Message = "You Made  A Bad Request" };
                    case HttpStatusCode.InternalServerError: return new() { Status = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTOStatus>(apiContent);
                        return apiResponseDTO;
                }
            }
            catch (Exception ex)
            {
                var dtoResponse = new ResponseDTOStatus
                {
                    Message = ex.Message.ToString(),
                    Status = false
                };
                return dtoResponse;
            }
        }
    }
}
