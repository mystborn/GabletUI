using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GabletUI.ViewModels;
using ReactiveUI;

namespace GabletUI.Views;

public partial class AccountView : ReactiveUserControl<AccountViewModel>
{
    public AccountView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}