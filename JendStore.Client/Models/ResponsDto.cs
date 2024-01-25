using Newtonsoft.Json;

namespace JendStore.Client.Models
{
    public class ResponsDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;

    }
}
