namespace JendStore.Client.Models
{
    public class ResponseDTOStatus
    {
        public object?  StatusResult { get; set; }
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}



//public object? Result { get; set; }
//public bool Success { get; set; } = false;
//public string? Message { get; set; } = string.Empty;
//public int Status { get; internal set; }