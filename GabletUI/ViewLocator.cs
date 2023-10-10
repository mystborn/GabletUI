using Avalonia.Controls;
using Avalonia.Controls.Templates;
using GabletUI.ViewModels;
using ReactiveUI;
using System;

namespace GabletUI
{
    public class ViewLocator : IDataTemplate, IViewLocator
    {
        public Control? Build(object? data)
        {
            if (data is null)
                return null;

            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = name };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }

        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            Control? viewFor = Build(viewModel);
            if (viewFor is not null)
                viewFor.DataContext = viewModel;

            return viewFor as IViewFor;
        }
    }
}