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
        public string? UrlPathSegment => "profile";

        public IScreen HostScreen { get; }

        public RoutingState Router { get; } = new RoutingState();

        public ProfileViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;

            Router.Navigate.Execute(new SignInViewModel(this));
        }
    }
}
