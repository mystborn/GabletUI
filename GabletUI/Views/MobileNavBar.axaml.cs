using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DynamicData.Binding;
using GabletUI.Models;
using GabletUI.ViewModels;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GabletUI.Views;

public partial class MobileNavBar : UserControl
{
    public static readonly StyledProperty<ICommand> TabSelectedProperty = AvaloniaProperty.Register<MobileNavBar, ICommand>(
        nameof(TabSelected));

    public static readonly StyledProperty<ObservableCollection<TabItemModel>> TabsProperty = AvaloniaProperty.Register<MobileNavBar, ObservableCollection<TabItemModel>>(
        nameof(TabSelected), new ObservableCollection<TabItemModel>());

    public static readonly StyledProperty<int> SelectedIndexProperty = AvaloniaProperty.Register<MobileNavBar, int>(
        nameof(SelectedIndex));

    public ObservableCollection<TabItemModel> Tabs
    {
        get => GetValue(TabsProperty);
        set => SetValue(TabsProperty, value);
    }

    public ICommand TabSelected
    {
        get => GetValue(TabSelectedProperty);
        set => SetValue(TabSelectedProperty, value);
    }

    public int SelectedIndex
    {
        get => GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public MobileNavBar()
    {
        // DataContext = new MobileNavBarViewModel();
        InitializeComponent();
    }

    public void TabIconClicked(object sender, RoutedEventArgs args)
    {
        var control = (StyledElement)sender;
        while (control.Parent is not null && control.Parent is not ItemsControl)
            control = control.Parent;

        if (control.Parent is null)
            return;

        var items = (ItemsControl)control.Parent;
        var panel = items.ItemsPanelRoot;
        var index = panel.Children.IndexOf((Control)control);

        SelectedIndex = index;

        if(TabSelected?.CanExecute(index) == true)
        {
            TabSelected.Execute(index);
        }
    }
}