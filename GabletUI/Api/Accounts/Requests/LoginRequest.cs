﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Requests;

public class LoginRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public LoginRequest()
    {
    }
}
