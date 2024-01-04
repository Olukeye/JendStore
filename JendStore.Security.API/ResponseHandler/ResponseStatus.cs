using Newtonsoft.Json;

namespace JendStore.Security.Service.API.ResponseHandler
{
    public class ResponseStatus
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = string.Empty;
        public int StatusCode { get; internal set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
