namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia.Data.Converters;

    public abstract class UltrasoundConvert : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public double DistanceConvert(Ultrasound i)
        {
            Ultrasound us = i;
            double max = us.maxReach;
            double val = us.Distance;
            if (val == max + 1)
            {
                return 0;
            }

            return (max - val) / max * 100;
        }

        public bool YellowThreshold(Ultrasound i)
        {
            Ultrasound us = i;
            double max = us.maxReach;
            double val = us.Distance;
            if (val == max + 1)
            {
                return false;
            }

            return val / 50 < 0.8;
        }

        public bool RedThreshold(Ultrasound i)
        {
            Ultrasound us = i;
            double max = us.maxReach;
            double val = us.Distance;
            if (val == max + 1)
            {
                return false;
            }

            return val / 50 < 0.4;
        }

        public string ColorSwitcher(Ultrasound i, string red, string yellow, string green)
        {
            if (this.RedThreshold(i))
            {
                return red;
            }
            else if (this.YellowThreshold(i))
            {
                return yellow;
            }
            else
            {
                return green;
            }
        }
    }
}
