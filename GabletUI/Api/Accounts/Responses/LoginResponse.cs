using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api.Accounts.Responses
{
    public class LoginResponse : IResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public ulong RefreshExpires { get; set; }
        public ulong AccessExpires { get; set; }
        public string? Username { get; set; }
        public int UserId { get; set; }
        public ErrorResponse? Error { get; set; }
    }
}
