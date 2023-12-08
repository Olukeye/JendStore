﻿using Newtonsoft.Json;

namespace JendStore.Services.API.DTO
{
    public class ResponseDTOStatus
    {
        public object?StatusResult { get; set; }
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}