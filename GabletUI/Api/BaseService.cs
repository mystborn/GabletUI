using GabletUI.Api.Accounts;
using GabletUI.Store;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public abstract class BaseService
    {
        protected HttpClient Client { get; }
        protected AuthStore Auth { get; }
        protected AccountService Account { get; }

        public BaseService(HttpClient client, AuthStore auth, AccountService? account)
        {
            Client = client;
            Auth = auth;

            if (account is not null)
            {
                Account = account;
            } 
            else if (this is AccountService accountService)
            {
                Account = accountService;
            }
            else
            {
                throw new ArgumentNullException(nameof(account));
            }
        }

        protected async Task<T> GetResource<T>(string url, [CallerMemberName] string? caller = null) where T : IResponse, new()
        {
            return await GetResourceImpl<T>(url, false, caller);
        }

        protected async Task<TResponse> GetResource<TResponse, TRequest>(string url, TRequest request, [CallerMemberName] string? caller = null)
            where TResponse : IResponse, new()
        {
            return await GetResourceImpl<TResponse, TRequest>(url, false, request, caller);
        }

        protected async Task<T> GetAuthorizedResource<T>(string url, [CallerMemberName] string? caller = null) where T : IResponse, new()
        {
            return await GetResourceImpl<T>(url, true, caller);
        }

        protected async Task<TResponse> GetAuthorizedResource<TResponse, TRequest>(string url, TRequest request, [CallerMemberName] string? caller = null)
            where TResponse : IResponse, new()
        {
            return await GetResourceImpl<TResponse, TRequest>(url, true, request, caller);
        }

        private async Task<T> GetResourceImpl<T>(string url, bool useAuth, string? caller = null) where T : IResponse, new()
        {
            if (useAuth)
                await Refresh();

            try
            {
                var bearer = Auth.AccessToken;
                if(string.IsNullOrWhiteSpace(bearer))
                    throw new InvalidOperationException("Cannot make request because the user is not logged in.");

                var message = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };

                if(useAuth)
                    message.Headers.Add("Authorization", $"Bearer {bearer}");

                var response = await Client.GetStringAsync(url);
                var result = JsonSerializer.Deserialize<T>(response)
                    ?? throw new InvalidOperationException($"Failed to deserialize GET result: {caller}. Response: {response}");

                return result;
            }
            catch (Exception ex)
            {
                return new T()
                {
                    Error = new ErrorResponse($"Failed to GET request: {caller}", ex),
                };
            }
        }

        private async Task<TResponse> GetResourceImpl<TResponse, TRequest>(string url, bool useAuth, TRequest request, string? caller = null)
            where TResponse : IResponse, new()
        {
            if(useAuth)
                await Refresh();

            try
            {
                var bearer = Auth.AccessToken;
                if (string.IsNullOrWhiteSpace(bearer))
                    throw new InvalidOperationException("Cannot make request because the user is not logged in.");

                var message = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Content = JsonContent.Create(
                        request,
                        new MediaTypeHeaderValue(MediaTypeNames.Application.Json))
                };

                message.Headers.Add("Content-Type", "application/json");
                if (useAuth)
                    message.Headers.Add("Authorization", $"Bearer {bearer}");

                var response = await Client.SendAsync(message);
                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<TResponse>(responseString)
                    ?? throw new InvalidOperationException($"Failed to deserialize GET result: {caller}. Response: {response}");

                return result;
            }
            catch (Exception ex)
            {
                return new TResponse()
                {
                    Error = new ErrorResponse($"Failed to GET request: {caller}", ex),
                };
            }
        }

        private async Task Refresh()
        {
            if (Auth.IsLoginExpired)
            {
                if (Auth.IsRefreshExpired)
                    throw new LoginExpiredException();

                await Account.Refresh(Auth.RefreshToken);
            }
        }
    }
}
