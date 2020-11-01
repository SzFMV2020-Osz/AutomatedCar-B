using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Views.Converters
{
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media;

    public class IntegerToAngleRpmConverter : IValueConverter
    {
        private const double MaxAngle = 260;
        private const double MaxRpm = 6000;
        private const double Shifting = 40;

        public IntegerToAngleRpmConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((int)value * MaxAngle / MaxRpm) - Shifting;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}