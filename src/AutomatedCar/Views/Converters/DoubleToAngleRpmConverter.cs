using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Views.Converters
{
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media;

    public class DoubleToAngleRpmConverter : IValueConverter
    {
        private static readonly double MaxAngle = 260;
        private static readonly double MaxRpm = 100;
        private static readonly double Shifting = 40;

        public DoubleToAngleRpmConverter()
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