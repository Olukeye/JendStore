using Newtonsoft.Json;

namespace JendStore.Security.Service.API.ResponseHandler
{
    public class ResponseStatus
    {
        public int StatusCode { get; set; } = 500;
        public string? Status { get; set; }
        public string? Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);

    }
}