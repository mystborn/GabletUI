﻿using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Models
{
    public class TabItemModel : ReactiveObject
    {
        private string? _icon;
        private string? _title;
        private bool _enabled;
        private Dock _titlePosition;
        private int _index;
        private Func<IRoutableViewModel>? _getView;

        public string? Icon
        {
            get => _icon;
            set => this.RaiseAndSetIfChanged(ref _icon, value);
        }

        public string? Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public bool Enabled
        {
            get => _enabled;
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }

        public Dock TitlePosition
        {
            get => _titlePosition;
            set => this.RaiseAndSetIfChanged(ref _titlePosition, value);
        }

        public int Index
        {
            get => _index;
            set => this.RaiseAndSetIfChanged(ref _index, value);
        }

        public Func<IRoutableViewModel>? GetView
        {
            get => _getView;
            set => this.RaiseAndSetIfChanged(ref _getView, value);
        }

        public TabItemModel(string? icon = null, string? title = null, bool enabled = true, Dock titlePosition = Dock.Bottom, int index = 0, Func<IRoutableViewModel>? getView = null)
        {
            _icon = icon;
            _title = title;
            _enabled = enabled;
            _titlePosition = titlePosition;
            _index = index;
            _getView = getView;
        }
    }
}
