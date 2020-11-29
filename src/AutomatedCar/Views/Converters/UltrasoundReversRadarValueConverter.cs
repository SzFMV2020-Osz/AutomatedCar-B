namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;

    class UltrasoundReversRadarValueConverter : UltrasoundConvert
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Ultrasound leftUltra = (value as AutomatedCar).Ultrasounds[6];
            Ultrasound rightUltra = (value as AutomatedCar).Ultrasounds[4];

            var d1 = leftUltra.Distance;
            var d2 = rightUltra.Distance;

            if (d1 == leftUltra.maxReach + 1)
            {
                d1 = leftUltra.maxReach / 50;
            }

            if (d2 == rightUltra.maxReach + 1)
            {
                d2 = leftUltra.maxReach / 50;
            }

            d1 /= 50;
            d2 /= 50;

            return Math.Min(d1, d2);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
