using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Responses
{
    public class ValidateAccountResponse : IResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ErrorResponse? Error { get; set; }
    }
}
