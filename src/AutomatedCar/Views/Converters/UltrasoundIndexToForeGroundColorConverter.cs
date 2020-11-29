namespace AutomatedCar.Views.Converters
{
    using AutomatedCar.SystemComponents;
    using System;
    using System.Globalization;

    public class UltrasoundIndexToForeGroundColorConverter : UltrasoundConvert
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => this.ColorSwitcher((Ultrasound)value, "Red", "Yellow", "Green");

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
