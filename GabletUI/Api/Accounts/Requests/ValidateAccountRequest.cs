using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Requests;

public class ValidateAccountRequest
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    public ValidateAccountRequest(string token, string username)
    {
        Token = token;
        Username = username;
    }

    public ValidateAccountRequest()
    {
    }
}
