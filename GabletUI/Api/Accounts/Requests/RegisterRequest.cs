using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Requests;

public class RegisterRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    public RegisterRequest(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }

    public RegisterRequest()
    {
    }
}
