using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class ComicsViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment => "index";

        public IScreen HostScreen { get; private set; }

        public ComicsViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }


    }
}
