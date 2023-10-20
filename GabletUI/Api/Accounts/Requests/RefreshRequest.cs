using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Requests;

public class RefreshRequest
{
    [JsonPropertyName("refresh")]
    public string RefreshToken { get; set; } = string.Empty;

    public RefreshRequest(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public RefreshRequest()
    {
    }
}
