using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class RegisterViewModel : ViewModelBase, IRoutableViewModel
    {
        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _error = string.Empty;
        private bool _isErrorVisible = false;

        public string? UrlPathSegment => "profile/register";

        public IScreen HostScreen { get; }

        public RegisterViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }
    }
}
