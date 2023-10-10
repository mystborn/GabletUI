using GabletUI.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class HomeViewModel : ViewModelBase, IScreen
    {
        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        public ObservableCollection<TabItemModel> Tabs { get; } = new()
        {
            
        };

        public ReactiveCommand<object, Unit> OnTabSelected { get; private set; }

        public RoutingState Router { get; } = new RoutingState();

        public HomeViewModel()
        {
            OnTabSelected = ReactiveCommand.Create<object>(obj =>
            {
                var tab = (TabItemModel)obj;
                Router.NavigateAndReset.Execute((IRoutableViewModel)tab.Context);
            });

            var home = new ComicsViewModel(this);

            Tabs = new ObservableCollection<TabItemModel>()
            {
                new TabItemModel("fa-solid fa-house", index: 0, context: home),
                new TabItemModel("fa-solid fa-user", index: 1, context: new ProfileViewModel(this)),
                new TabItemModel("fa-solid fa-bell", index: 2, context: new NotificationsViewModel(this)),
                new TabItemModel("fa-solid fa-magnifying-glass", index: 3, context: new SearchViewModel(this))
            };

            Router.NavigateAndReset.Execute(home);
        }
    }
}
