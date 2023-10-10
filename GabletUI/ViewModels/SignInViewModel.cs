using Avalonia.Threading;
using GabletUI.Api;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GabletUI.ViewModels
{
    public class SignInViewModel : ViewModelBase, IRoutableViewModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _error = string.Empty;
        private bool _isErrorVisible = false;

        public string? UrlPathSegment => "profile/signin";

        public IScreen HostScreen { get; }

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Error
        {
            get => _error;
            set
            {
                if (value != _error)
                {
                    this.RaiseAndSetIfChanged(ref _error, value);
                    IsErrorVisible = !string.IsNullOrWhiteSpace(value);
                }
            }
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => this.RaiseAndSetIfChanged(ref _isErrorVisible, value);
        }

        public ICommand SignIn { get; }

        public SignInViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;

            SignIn = ReactiveCommand.CreateFromTask(async () =>
            {
                var accountService = App.Services.GetService<IAccountService>();
                try
                {
                    var result = await accountService.Login(Username, Password);
                    { }
                }
                catch(Exception ex)
                {
                    Error = ex.ToString();
                }
            });
        } 
    }
}
