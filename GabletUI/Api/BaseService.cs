using GabletUI.Api.Accounts;
using GabletUI.Store;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task Get(string url)
            => await SendRequestImpl<object>(url, false, null, HttpMethod.Get);

        public async Task<HttpResponseMessage> GetResponse(string url)
            => await SendRequestImpl<object>(url, false, null, HttpMethod.Get);

        public async Task<HttpResponseMessage> GetAuthorizedResponse(string url)
            => await SendRequestImpl<object>(url, true, null, HttpMethod.Get);

        public async Task<HttpResponseMessage> GetResponse<T>(string url, T request)
            => await SendRequestImpl<T>(url, false, request, HttpMethod.Get);

        public async Task<HttpResponseMessage> GetAuthorizedResponse<T>(string url, T request)
            => await SendRequestImpl<T>(url, true, request, HttpMethod.Get);

        public async Task<string> GetString(string url)
            => await SendRequestGetString<object>(url, false, null, HttpMethod.Get);

        public async Task<string> GetString<T>(string url, T request)
            => await SendRequestGetString(url, false, request, HttpMethod.Get);

        public async Task<string> GetAuthorizedString(string url)
            => await SendRequestGetString<object>(url, true, null, HttpMethod.Get);

        public async Task<string> GetAuthorizedString<T>(string url, T request)
            => await SendRequestGetString(url, true, request, HttpMethod.Get);

        protected async Task<T> GetResource<T>(string url) where T : IResponse, new()
        {
            return await SendRequest<T, object>(url, false, null, HttpMethod.Get);
        }

        protected async Task<TResponse> GetResource<TResponse, TRequest>(string url, TRequest request)
        {
            return await SendRequest<TResponse, TRequest>(url, false, request, HttpMethod.Get);
        }

        protected async Task<T> GetAuthorizedResource<T>(string url) where T : IResponse, new()
        {
            return await SendRequest<T, object>(url, true, null, HttpMethod.Get);
        }

        protected async Task<TResponse> GetAuthorizedResource<TResponse, TRequest>(string url, TRequest request)
        {
            return await SendRequest<TResponse, TRequest>(url, true, request, HttpMethod.Get);
        }

        public async Task Post(string url)
            => await SendRequestImpl<object>(url, false, null, HttpMethod.Post);

        public async Task<HttpResponseMessage> PostResponse(string url)
            => await SendRequestImpl<object>(url, false, null, HttpMethod.Post);

        public async Task<HttpResponseMessage> PostAuthorizedResponse(string url)
            => await SendRequestImpl<object>(url, true, null, HttpMethod.Post);

        public async Task<HttpResponseMessage> PostResponse<T>(string url, T request)
            => await SendRequestImpl<T>(url, false, request, HttpMethod.Post);

        public async Task<HttpResponseMessage> PostAuthorizedResponse<T>(string url, T request)
            => await SendRequestImpl<T>(url, true, request, HttpMethod.Post);

        public async Task<string> PostString(string url)
            => await SendRequestGetString<object>(url, false, null, HttpMethod.Post);

        public async Task<string> PostString<T>(string url, T request)
            => await SendRequestGetString(url, false, request, HttpMethod.Post);

        public async Task<string> PostAuthorizedString(string url)
            => await SendRequestGetString<object>(url, true, null, HttpMethod.Post);

        public async Task<string> PostAuthorizedString<T>(string url, T request)
            => await SendRequestGetString(url, true, request, HttpMethod.Post);

        protected async Task<T> PostResource<T>(string url) where T : IResponse, new()
        {
            return await SendRequest<T, object>(url, false, null, HttpMethod.Post);
        }

        protected async Task<TResponse> PostResource<TResponse, TRequest>(string url, TRequest request)
        {
            return await SendRequest<TResponse, TRequest>(url, false, request, HttpMethod.Post);
        }

        protected async Task<T> PostAuthorizedResource<T>(string url) where T : IResponse, new()
        {
            return await SendRequest<T, object>(url, true, null, HttpMethod.Post);
        }

        protected async Task<TResponse> PostAuthorizedResource<TResponse, TRequest>(string url, TRequest request)
        {
            return await SendRequest<TResponse, TRequest>(url, true, request, HttpMethod.Post);
        }

        private async Task<TResponse> SendRequest<TResponse, TRequest>(string url, bool useAuth, TRequest? request, HttpMethod method)
        {
            var responseString = await SendRequestGetString(url, useAuth, request, method);
            
            var result = JsonSerializer.Deserialize<TResponse>(responseString)
                ?? throw new InvalidOperationException($"Failed to deserialize response content: {responseString}");

            return result;
        }

        private async Task<string> SendRequestGetString<TRequest>(string url, bool useAuth, TRequest? request, HttpMethod method)
        {
            var response = await SendRequestImpl(url, useAuth, request, method);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ErrorResponse error;
                try
                {
                    error = JsonSerializer.Deserialize<ErrorResponse>(responseString)!;
                }
                catch (Exception ex)
                {
                    error = new ErrorResponse()
                    {
                        ErrorMessage = $"Http request to {url} failed",
                        ErrorCode = (int)response.StatusCode
                    };
                }
                throw new GabletApiException(error);
            }

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> SendRequestImpl<TRequest>(string url, bool useAuth, TRequest? request, HttpMethod method)
        {
            if (useAuth)
            {
                await Refresh();
                if (string.IsNullOrWhiteSpace(Auth.AccessToken))
                    throw new InvalidOperationException("Cannot make request because the user is not logged in.");
            }

            var message = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(url),
                Content = request is null ? null : JsonContent.Create(
                        request,
                        new MediaTypeHeaderValue(MediaTypeNames.Application.Json))
            };

            //if (request is not null)
            //    message.Headers.Add("Content-Type", "application/json");

            if (useAuth)
            {
                message.Headers.Add("Authorization", $"Bearer {Auth.AccessToken}");
            }

            return await Client.SendAsync(message);
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
