namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media.Imaging;

    class LastSeenSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == String.Empty)
            {
                return new Bitmap(@"..\..\..\\Assets\no_sign.png");
            }
            else
            {
                return new Bitmap(@"..\..\..\\Assets\WorldObjects\" + "roadsign_speed_60" + ".png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
