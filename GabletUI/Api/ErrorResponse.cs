using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public class ErrorResponse
    {
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("error_type")]
        public string? ErrorType { get; set; }

        [JsonPropertyName("stack_trace")]
        public string? StackTrace { get; set; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(string message)
        {
            ErrorMessage = message;
            ErrorCode = 500;
        }

        public ErrorResponse(string message, Exception exception)
        {
            ErrorMessage = message;
            ErrorCode = 500;
            StackTrace = exception.StackTrace;
        }
    }
}
