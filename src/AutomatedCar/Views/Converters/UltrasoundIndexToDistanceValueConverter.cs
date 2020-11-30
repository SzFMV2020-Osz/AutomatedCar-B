namespace AutomatedCar.Views.Converters
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class UltrasoundIndexToDistanceValueConverter : UltrasoundConvert
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Ultrasound[] ultras = World.Instance.ControlledCar.Ultrasounds;
            if (ultras.Where(x => x.Points == (List<Point>)value).FirstOrDefault() != null)
            {
                double distance = ultras.Where(x => x.Points == (List<Point>)value).FirstOrDefault().Distance;
                return this.DistanceConvert(distance);
            }
            else
            {
                return 0;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object();
        }
    }
}
