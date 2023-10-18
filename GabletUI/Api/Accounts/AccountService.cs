using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using GabletUI.Api.Accounts.Requests;
using GabletUI.Api.Accounts.Responses;
using GabletUI.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using GabletUI.Services.Store;

namespace GabletUI.Api.Accounts
{
    public class AccountService : BaseService, IAccountService
    {
        private const string REGISTER = $"{ApiPaths.AUTH_SERVER}/api/register";
        private const string VALIDATE_ACCOUNT = $"{ApiPaths.AUTH_SERVER}/api/validate";
        private const string LOGIN = $"{ApiPaths.AUTH_SERVER}/api/login";
        private const string REFRESH = $"{ApiPaths.AUTH_SERVER}/api/refresh";
        private const string PING = $"{ApiPaths.AUTH_SERVER}/api/ping";

        private JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();

        public AccountService(HttpClient httpClient, AuthStore auth)
            : base(httpClient, auth, null)
        {
        }

        public async Task Register(string username, string email, string password)
        {
            var request = new RegisterRequest(username, email, password);

            var response = await PostResource<LoginResponse, RegisterRequest>(REGISTER, request);
            if (response.Error is not null)
                throw new GabletApiException(response.Error);

            SetAuthValues(response);
        }

        public async Task<bool> ValidateAccount(string token, string username)
        {
            var request = new ValidateAccountRequest(token, username);
            var response = await PostResource<ValidateAccountResponse, ValidateAccountRequest>(VALIDATE_ACCOUNT, request);
            if (response.Error is not null)
                throw new GabletApiException(response.Error);

            return response.Success;
        }

        public async Task Login(string username, string password)
        {
            var request = new LoginRequest(username, password);
            var response = await PostResource<LoginResponse, LoginRequest>(LOGIN, request);
            if (response.Error is not null)
                throw new GabletApiException(response.Error);

            SetAuthValues(response);
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
            var access = _jwtHandler.ReadJwtToken(login.AccessToken);
            var refresh = _jwtHandler.ReadJwtToken(login.RefreshToken);
            var id = access.Payload["user_id"];

            Auth.Username = access.Subject!;
            Auth.RefreshToken = login.RefreshToken!;
            Auth.AccessToken = login.AccessToken!;
            Auth.RefreshExpires = (ulong)((DateTimeOffset)refresh.ValidTo).ToUnixTimeSeconds();
            Auth.LoginExpires = (ulong)((DateTimeOffset)access.ValidTo).ToUnixTimeSeconds();
            Auth.UserId = (int)id;
        }
    }
}
