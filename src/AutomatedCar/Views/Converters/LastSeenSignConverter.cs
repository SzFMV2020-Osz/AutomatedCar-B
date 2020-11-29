namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;

    class LastSeenSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == String.Empty)
            {
                return "resm:AutomatedCar.Assets.no_sign.png?assembly=AutomatedCar";
            }
            else
            {
                return "resm:AutomatedCar.Assets.WorldObjects" + (string)value + "?assembly=AutomatedCar";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
