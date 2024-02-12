using Newtonsoft.Json;

namespace JendStore.Products.Service.API.ResponseHandler
{
    public class ResponseStatus
    {
        public string? Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public int StatusCode { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
