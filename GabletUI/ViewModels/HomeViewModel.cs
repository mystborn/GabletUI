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
                var view = tab.GetView?.Invoke();
                if(view is not null)
                    Router.NavigateAndReset.Execute(view);
            });

            Tabs =
            [
                new("fa-solid fa-house", index: 0, getView: () => new ComicsViewModel(this)),
                new("fa-solid fa-user", index: 1, getView: () => new ProfileViewModel(this)),
                new("fa-solid fa-bell", index: 2, getView: () => new NotificationsViewModel(this)),
                new("fa-solid fa-magnifying-glass", index: 3, getView: () => new SearchViewModel(this))
            ];

            Router.NavigateAndReset.Execute(Tabs[0].GetView!());
        }
    }
}
