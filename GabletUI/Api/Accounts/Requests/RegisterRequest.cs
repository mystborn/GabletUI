﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Requests
{
    public record RegisterRequest(string Username, string Email, string Password);
}
