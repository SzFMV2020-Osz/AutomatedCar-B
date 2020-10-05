using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AutomatedCar.Visualization
{
    public class TranslateSetback : IValueConverter
    {
        public static TranslateSetback Instance { get; } = new TranslateSetback();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            var val = value;

            return 500;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
