using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Models
{
    public class GridUtils : AvaloniaObject
    {
        public static readonly AttachedProperty<int> ItemsPerColumnProperty = AvaloniaProperty.RegisterAttached<GridUtils, Grid, int>(
            "ItemsPerColumn", 0, false);

        public static readonly AttachedProperty<int> ItemsPerRowProperty = AvaloniaProperty.RegisterAttached<GridUtils, Grid, int>(
            "ItemsPerRow", 0, false);

        public static void SetItemsPerColumn(AvaloniaObject element, int itemsPerColumn)
        {
            element.SetValue(ItemsPerColumnProperty, itemsPerColumn);
        }

        public static void SetItemsPerRow(AvaloniaObject element, int itemsPerRow)
        {
            element.SetValue(ItemsPerRowProperty, itemsPerRow);
        }

        public static int GetItemsPerColumn(AvaloniaObject element)
        {
            return element.GetValue(ItemsPerColumnProperty);
        }

        public static int GetItemsPerRow(AvaloniaObject element)
        {
            return element.GetValue(ItemsPerRowProperty);
        }

        static GridUtils()
        {
            ItemsPerColumnProperty.Changed.Subscribe(OnItemsPerColumnPropertyChanged);
            ItemsPerRowProperty.Changed.Subscribe(OnItemsPerColumnPropertyChanged);
        }

        public static void OnItemsPerColumnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var grid = e.Sender as Grid;
            var itemsPerColumn = e.GetNewValue<int>();

            grid.LayoutUpdated += (s, e2) =>
            {
                var childCount = grid.Children.Count;
                var columnsToAdd = (childCount - grid.ColumnDefinitions.Count) / itemsPerColumn;
                for (var i = 0; i < columnsToAdd; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
                }

                for (var i = 0; i < childCount; i++)
                {
                    var child = grid.Children[i];
                    Grid.SetColumn(child, i / itemsPerColumn);
                }
            };
        }
    }
}
