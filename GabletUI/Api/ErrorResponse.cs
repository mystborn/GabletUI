using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorType { get; set; }
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
