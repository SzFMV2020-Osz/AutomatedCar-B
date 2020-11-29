namespace AutomatedCar.Views.Converters
{
    using AutomatedCar.SystemComponents;
    using System;
    using System.Globalization;

    public class UltrasoundIndexToBackGroundColorConverter : UltrasoundConvert
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => this.ColorSwitcher((Ultrasound)value, "Coral", "LightYellow", "LightGreen");

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
