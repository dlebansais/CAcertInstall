namespace Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Represents a converter from a <see cref="bool"/> instance to a <see cref="Visibility"/> instance.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToObjectConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="bool"/> value to a <see cref="Visibility"/> instance in a binding.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="target_type">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
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

            return BooleanValue ? VisibilityList[1] : VisibilityList[0];
        }

        /// <summary>
        /// Converts a <see cref="Visibility"/> value to a <see cref="bool"/> instance in a binding.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="target_type">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
