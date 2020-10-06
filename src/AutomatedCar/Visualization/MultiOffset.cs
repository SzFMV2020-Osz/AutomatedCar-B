namespace AutomatedCar.Visualization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using Avalonia;
    using Avalonia.Data.Converters;

    public class MultiOffset : IMultiValueConverter
    {

        public static MultiOffset Instance { get; } = new MultiOffset();

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            int x = (int)values[0];
            int offset = (int)values[1];
            return x + offset;
        }

        // public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        // {
        //     throw new NotSupportedException();
        // }
    }
}