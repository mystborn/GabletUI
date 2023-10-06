using GabletUI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public interface IAccountService
    {
        public Task<UserModel> Register(string username, string email, string password);
        public Task<bool> ValidateAccount(string token, string username);
        public Task<UserModel> Login(string username, string password);
        public Task Refresh(string refreshToken);
    }
}
