using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class SearchViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment => "search";

        public IScreen HostScreen { get; }

        public SearchViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }
    }
}
