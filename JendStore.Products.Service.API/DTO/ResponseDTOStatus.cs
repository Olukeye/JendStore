using Newtonsoft.Json;

namespace JendStore.PRoducts.Service.API.DTO
{
    public class ResponseDTOStatus
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

