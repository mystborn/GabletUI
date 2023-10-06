using GabletUI.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.ViewModels
{
    public class MobileNavBarViewModel : ViewModelBase
    {
        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        public TabItemModel[] Tabs { get; } =
        [
            new TabItemModel("fa-solid fa-house", index: 0),
            new TabItemModel("fa-solid fa-user", index: 1),
            new TabItemModel("fa-solid fa-bell", index: 2),
            new TabItemModel("fa-solid fa-magnifying-glass", index: 3),
        ];
    }
}
