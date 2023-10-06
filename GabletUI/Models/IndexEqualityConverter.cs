using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Models
{
    public class IndexEqualityConverter : IMultiValueConverter
    {
        public static readonly IndexEqualityConverter Instance = new IndexEqualityConverter();

        public IndexEqualityConverter() { }

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count != 2)
                throw new ArgumentException("Expected two arguments to the IndexConvertor", nameof(values));

            var itemIndex = values[0];
            var selectedIndex = values[1];

            if (itemIndex is int left && selectedIndex is int right)
                return left == right;

            return true;
        }
    }
}
