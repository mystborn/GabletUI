using GabletUI.Models;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace GabletUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _content;

        public ViewModelBase ContentViewModel
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public MainViewModel()
        {
            var home = App.Services.GetService<HomeViewModel>()!;
            _content = home;
        }
    }
}
