namespace JendStore.Cart.Service.API.DTO
{
    public class ResponseDTOStatus
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }

    }
}

