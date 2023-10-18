using GabletUI.Api;
using GabletUI.Services.Validation;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GabletUI.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase, IRoutableViewModel
    {
        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _generalError = string.Empty;
        private bool _isErrorVisible = false;
        private IValidationService _validationService;

        public string? UrlPathSegment => "profile/register";

        public IScreen HostScreen { get; }

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
        }

        public string GeneralError
        {
            get => _generalError;
            set
            {
                if (value != _generalError)
                {
                    this.RaiseAndSetIfChanged(ref _generalError, value);
                    IsGeneralErrorVisible = !string.IsNullOrWhiteSpace(value);
                }
            }
        }

        public bool IsGeneralErrorVisible
        {
            get => _isErrorVisible;
            set => this.RaiseAndSetIfChanged(ref _isErrorVisible, value);
        }

        public ICommand Register { get; }
        public ICommand SwitchToSignIn { get; }

        public RegisterViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            _validationService = App.Services.GetService<IValidationService>()!;

            var register = ReactiveCommand.CreateFromTask(async () =>
            {
                ClearAllErrors();

                if (!ValidateInputClientSide())
                    return;

                var accountService = App.Services.GetService<IAccountService>()!;
                var success = false;
                try
                {
                    await accountService.Register(Username, Email, Password);
                    success = true;
                }
                catch(GabletApiException ex)
                {
                    GeneralError = $"Error {ex.Response.ErrorCode}: {ex.Response.ErrorMessage}";
                }
                catch(Exception ex)
                {
                    GeneralError = $"Error: {ex}";
                }
            });

            Register = register;

            SwitchToSignIn = ReactiveCommand.Create(() =>
            {
                HostScreen.Router.NavigateAndReset.Execute(new SignInViewModel(HostScreen));
            },
            register.IsExecuting.Select(executing => !executing));
        }

        private bool ValidateInputClientSide()
        {
            var result = true;

            if (!_validationService.IsValidUsername(Username, out var errors))
            {
                result = false;
                SetErrors(nameof(Username), errors);
            }

            if(!_validationService.IsValidEmail(Email, out errors))
            {
                result = false;
                SetErrors(nameof(Email), errors);
            }

            if (!_validationService.IsValidPassword(Password, out errors))
            {
                result = false;
                SetErrors(nameof(Password), errors);
            }

            if (Password != ConfirmPassword)
            {
                result = false;
                SetErrors(nameof(ConfirmPassword), new[] { "Confirm Password does not match Password" });
            }

            return result;
        }
    }
}
