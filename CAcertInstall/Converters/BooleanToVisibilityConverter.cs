#nullable enable

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type target_type, object parameter, CultureInfo culture)
        {
            bool BooleanValue = (bool)value;
            CompositeCollection VisibilityList = (CompositeCollection)parameter;
            return BooleanValue ? VisibilityList[0] : VisibilityList[1];
        }

        public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
