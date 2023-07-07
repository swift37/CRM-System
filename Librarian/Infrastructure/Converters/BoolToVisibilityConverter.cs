using Librarian.Infrastructure.Converters;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Librarian.Infrastructure.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    [MarkupExtensionReturnType(typeof(Visibility))]
    public class BoolToVisibilityConverter : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool state)) throw new InvalidCastException(nameof(value));
            if (state) return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility state)) throw new InvalidCastException(nameof(value));
            if (state is Visibility.Collapsed) return true;
            return false;
        }
    }
}
