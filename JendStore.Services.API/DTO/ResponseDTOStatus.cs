using Newtonsoft.Json;

namespace JendStore.Services.API.DTO
{
    public class ResponseDTOStatus
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

