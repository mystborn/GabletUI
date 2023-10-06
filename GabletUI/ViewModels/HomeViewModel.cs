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
    public class HomeViewModel : ViewModelBase
    {
        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        public ObservableCollection<TabItemModel> Tabs { get; } = new()
        {
            new TabItemModel("fa-solid fa-house", index: 0),
            new TabItemModel("fa-solid fa-user", index: 1),
            new TabItemModel("fa-solid fa-bell", index: 2),
            new TabItemModel("fa-solid fa-magnifying-glass", index: 3)
        };

        public ReactiveCommand<object, Unit> OnTabSelected { get; private set; }

        public HomeViewModel()
        {
            OnTabSelected = ReactiveCommand.Create<object>(tab =>
            {
                Console.WriteLine(tab);
            });

            var canExecute = OnTabSelected.CanExecute.FirstAsync().Wait();
        }
    }
}
