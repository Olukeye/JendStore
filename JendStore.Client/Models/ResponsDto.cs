using Newtonsoft.Json;

namespace JendStore.Security.Service.API.DTO
{
    public class ResponsDto
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = string.Empty;
    }
}
