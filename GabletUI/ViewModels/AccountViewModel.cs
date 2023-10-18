using GabletUI.Services.Store;
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
    public class AccountViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment => "profile/user";

        public IScreen HostScreen { get; }

        public string Username { get; }

        private AuthStore _authStore = App.Services.GetService<AuthStore>()!;

        public ICommand SignOut { get; }

        public AccountViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;

            if (_authStore.IsLoginExpired)
            {
            }

            Username = _authStore.Username;

            SignOut = ReactiveCommand.Create(() =>
            {
                _authStore.Reset();
                HostScreen.Router.NavigateAndReset.Execute(new SignInViewModel(HostScreen));
            });
        }
    }
}
