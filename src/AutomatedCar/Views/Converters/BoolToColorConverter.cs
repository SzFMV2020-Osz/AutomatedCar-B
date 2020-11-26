using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Views.Converters
{
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media;

    public class BoolToColorConverter : IValueConverter
    {
        public BoolToColorConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => System.Convert.ToBoolean(value) ? Brush.Parse("#FFFF00") : Brush.Parse("#FFFFFF");

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
