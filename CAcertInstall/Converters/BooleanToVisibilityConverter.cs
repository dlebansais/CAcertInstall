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
            if (!(value is bool))
                throw new ArgumentOutOfRangeException(nameof(value));
            if (!(parameter is CompositeCollection))
                throw new ArgumentOutOfRangeException(nameof(parameter));

            bool BooleanValue = (bool)value;
            CompositeCollection VisibilityList = (CompositeCollection)parameter;

            if (VisibilityList.Count < 2)
                throw new ArgumentOutOfRangeException(nameof(parameter));

            return BooleanValue ? VisibilityList[0] : VisibilityList[1];
        }

        public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
