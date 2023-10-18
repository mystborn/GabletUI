using Avalonia.Threading;
using GabletUI.Api;
using GabletUI.Services.Store;
using GabletUI.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class ProfileViewModel : ViewModelBase, IRoutableViewModel, IScreen
    {
        private bool _loading = false;

        public string? UrlPathSegment => "profile";

        public IScreen HostScreen { get; }

        public RoutingState Router { get; } = new RoutingState();

        public bool Loading
        {
            get => _loading;
            set => this.RaiseAndSetIfChanged(ref _loading, value);
        }

        public ProfileViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;

            var authStore = App.Services.GetService<AuthStore>()!;
            var requiresSignIn = authStore.Username == string.Empty;


            if(authStore.IsLoginExpired)
            {
                requiresSignIn = true;
                if(!authStore.IsRefreshExpired)
                {
                    Loading = true;
                    RefreshLogin(authStore);
                }
            }

            if(!Loading)
            {
                IRoutableViewModel vm = requiresSignIn ? new SignInViewModel(this) : new AccountViewModel(this);
                Router.NavigateAndReset.Execute(vm);
            }
        }

        private async Task RefreshLogin(AuthStore authStore)
        {
            var authService = App.Services.GetService<IAccountService>()!;
            var requiresSignIn = true;

            try
            {
                await authService.Refresh(authStore.RefreshToken);
                requiresSignIn = false;
            }
            catch(Exception ex)
            {
                var logger = App.Services.GetService<ILogger<ProfileViewModel>>()!;
                logger.LogDebug(ex, "Failed to refresh token");
            }

            Loading = false;
            IRoutableViewModel vm = requiresSignIn ? new SignInViewModel(this) : new AccountViewModel(this);
            Router.NavigateAndReset.Execute(vm);
        }
    }
}
