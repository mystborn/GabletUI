using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Models
{
    public class TabIndexColorConverter : IMultiValueConverter
    {
        private static readonly IndexEqualityConverter _indexChecker = new IndexEqualityConverter();

        public static readonly TabIndexColorConverter Instance = new TabIndexColorConverter();

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isIndex = (bool)_indexChecker.Convert(values, targetType, parameter, culture);
            if(isIndex && App.Current.TryGetResource("PrimaryColor", App.Current.RequestedThemeVariant, out var color))
            {
                return color;
            }
            else if(App.Current.TryGetResource("SecondaryColor", App.Current.RequestedThemeVariant, out color))
            {
                return color;
            }

            return null;
        }
    }
}
