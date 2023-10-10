using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class AccountViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment => "profile/user";

        public IScreen HostScreen { get; }

        public AccountViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }
    }
}
