namespace AutomatedCar.Views.Converters
{
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media;
    using System;

    public class DoubleToAngleKmHConverter : IValueConverter
    {
        private const double MaxAngle = 260;
        private const double MaxRpm = 200;
        private const double Shifting = 40;

        public DoubleToAngleKmHConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((double)value * MaxAngle / MaxRpm) - Shifting;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}