namespace AutomatedCar.Views.Converters
{
    using AutomatedCar.SystemComponents;
    using System;
    using System.Globalization;

    public class UltrasoundIndexToDistanceValueConverter : UltrasoundConvert
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => this.DistanceConvert((Ultrasound)value);

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object();
        }
    }
}
