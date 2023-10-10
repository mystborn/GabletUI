using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class NotificationsViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment => "notifications";

        public IScreen HostScreen { get; }

        public NotificationsViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }
    }
}
