using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using GabletUI.Store;
using GabletUI.Api.Accounts.Requests;
using GabletUI.Api.Accounts.Responses;
using GabletUI.Api.Models;

namespace GabletUI.Api.Accounts
{
    public class AccountService : BaseService, IAccountService
    {
        private const string REGISTER = $"{ApiPaths.AUTH_SERVER}/api/register";
        private const string VALIDATE_ACCOUNT = $"{ApiPaths.AUTH_SERVER}/api/validate";
        private const string LOGIN = $"{ApiPaths.AUTH_SERVER}/api/login";
        private const string REFRESH = $"{ApiPaths.AUTH_SERVER}/api/refresh";
        private const string PING = $"{ApiPaths.AUTH_SERVER}/api/ping";

        public AccountService(HttpClient httpClient, AuthStore auth)
            : base(httpClient, auth, null)
        {
        }

        public async Task<UserModel> Register(string username, string email, string password)
        {
            var request = new RegisterRequest(username, email, password);

            var response = await PostResource<LoginResponse, RegisterRequest>(REGISTER, request);
            if (response.Error is not null)
                throw new GabletApiException(response.Error);

            SetAuthValues(response);

            return new UserModel(response.Username!, response.UserId);
        }

        public async Task<bool> ValidateAccount(string token, string username)
        {
            var request = new ValidateAccountRequest(token, username);
            var response = await PostResource<ValidateAccountResponse, ValidateAccountRequest>(VALIDATE_ACCOUNT, request);
            if (response.Error is not null)
                throw new GabletApiException(response.Error);

            return response.Success;
        }

        public async Task<UserModel> Login(string username, string password)
        {
            var request = new LoginRequest(username, password);
            var response = await PostResource<LoginResponse, LoginRequest>(LOGIN, request);
            if (response.Error is not null)
                throw new GabletApiException(response.Error);

            SetAuthValues(response);

            return new UserModel(response.Username!, response.UserId);
        }

        public async Task Refresh(string token)
        {
            var request = new RefreshRequest(token);
            var response = await PostResource<LoginResponse, RefreshRequest>(REFRESH, request);
            if (response.Error is not null)
                throw new GabletApiException(response.Error);

            SetAuthValues(response);
        }

        public async Task<string> Test()
        {
            return await GetString(PING);
        }

        private void SetAuthValues(LoginResponse login)
        {
            Auth.Username = login.Username!;
            Auth.RefreshToken = login.RefreshToken!;
            Auth.AccessToken = login.AccessToken!;
            Auth.RefreshExpires = login.RefreshExpires;
            Auth.LoginExpires = login.AccessExpires;
            Auth.UserId = login.UserId;
        }
    }
}
