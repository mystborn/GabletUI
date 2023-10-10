using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GabletUI.ViewModels;
using ReactiveUI;

namespace GabletUI.Views;

public partial class NotificationsView : ReactiveUserControl<NotificationsViewModel>
{
    public NotificationsView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}