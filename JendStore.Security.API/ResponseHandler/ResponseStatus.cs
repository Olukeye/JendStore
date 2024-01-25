using Newtonsoft.Json;

namespace JendStore.Security.Service.API.ResponseHandler
{
    public class ResponseStatus
    {
        public object? StatusResult { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = string.Empty;
        public int Status { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
