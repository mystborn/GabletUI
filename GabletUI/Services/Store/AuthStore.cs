using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Services.Store
{
    public class AuthStore
    {
        private const string ACCESS_TOKEN = "Auth_AccessToken";
        private const string REFRESH_TOKEN = "Auth_RefreshToken";
        private const string LOGIN_EXPIRES = "Auth_LoginExpires";
        private const string REFRESH_EXPIRES = "Auth_RefreshExpires";
        private const string USERNAME = "Auth_Username";
        private const string USER_ID = "Auth_UserId";

        private IStorage _storage;
        private string _accessToken;
        private string _refreshToken;
        private ulong _loginExpires;
        private ulong _refreshExpires;
        private string _username;
        private int _userId;

        public string AccessToken
        {
            get => _accessToken;
            set
            {
                if (value != _accessToken)
                {
                    _storage.Set(ACCESS_TOKEN, value);
                    _accessToken = value;
                }
            }
        }

        public string RefreshToken
        {
            get => _refreshToken;
            set
            {
                if (value != _refreshToken)
                {
                    _storage.Set(REFRESH_TOKEN, value);
                    _refreshToken = value;
                }
            }
        }

        public ulong LoginExpires
        {
            get => _loginExpires;
            set
            {
                if (value != _loginExpires)
                {
                    _storage.Set(LOGIN_EXPIRES, value);
                    _loginExpires = value;
                }
            }
        }

        public ulong RefreshExpires
        {
            get => _refreshExpires;
            set
            {
                if (value != _refreshExpires)
                {
                    _storage.Set(REFRESH_EXPIRES, value);
                    _refreshExpires = value;
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _storage.Set(USERNAME, value);
                    _username = value;
                }
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                if (value != _userId)
                {
                    _storage.Set(USER_ID, value);
                    _userId = value;
                }
            }
        }

        public bool IsLoginExpired => (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= LoginExpires;

        public bool IsRefreshExpired => (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= RefreshExpires;

        public AuthStore(IStorage storage)
        {
            _storage = storage;
            _accessToken = storage.GetString(ACCESS_TOKEN);
            _refreshToken = storage.GetString(REFRESH_TOKEN);
            _loginExpires = storage.GetULong(LOGIN_EXPIRES);
            _refreshExpires = storage.GetULong(REFRESH_EXPIRES);
            _username = storage.GetString(USERNAME);
            _userId = storage.GetInt(USER_ID);
        }

        public void Reset()
        {
            AccessToken = "";
            RefreshToken = "";
            LoginExpires = 0;
            RefreshExpires = 0;
            Username = "";
            UserId = 0;
        }
    }
}
