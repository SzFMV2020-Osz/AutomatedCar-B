namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Avalonia.Data.Converters;
    using Avalonia.Media.Imaging;

    class LastSeenSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == String.Empty)
            {
                return new Bitmap(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.no_sign.png"));
            }
            else
            {
                return new Bitmap(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects." + (string)value + ".png"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
