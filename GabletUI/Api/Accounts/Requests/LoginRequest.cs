using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Requests
{
    public record LoginRequest(
        [property:JsonPropertyName("username")] string Username, 
        [property:JsonPropertyName("password")] string Password);
}
