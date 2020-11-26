namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;

    public class PixelsPerTickToKmHAngleConverter : IValueConverter
    {
        private const double MaxAngle = 260;
        private const double MaxKmpH = 200;
        private const double Shifting = 40;

        public PixelsPerTickToKmHAngleConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((3.6 * (double)value) / 50 * MaxAngle / MaxKmpH) - Shifting;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}