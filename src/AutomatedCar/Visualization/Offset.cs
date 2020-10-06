namespace AutomatedCar.Visualization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using Avalonia;
    using Avalonia.Data.Converters;
    using Avalonia.Media.Imaging;

    public class Offset : IValueConverter
    {

        public static Offset Instance { get; } = new Offset();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value - 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 100;
        }
    }
}