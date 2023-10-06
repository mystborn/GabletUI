using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Requests
{
    public record ValidateAccountRequest(string Token, string Username);
}
