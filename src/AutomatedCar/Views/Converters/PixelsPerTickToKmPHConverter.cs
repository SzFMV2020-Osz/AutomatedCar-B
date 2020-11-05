namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;

    class PixelsPerTickToKmPHConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (double)value * 3.6 * 60;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
