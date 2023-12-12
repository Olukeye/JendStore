namespace JendStore.Client.Models
{
    public class ResponseDTOStatus
    {
        public int StatusCode { get; set; }
        public object StatusResult { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}