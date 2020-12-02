namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Globalization;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia.Data.Converters;
    using Avalonia.Media;

    public abstract class UltrasoundConvert : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public double DistanceConvert(double i)
        {
            if (World.Instance.ControlledCar.VirtualFunctionBus.HMIPacket.Gear != Gears.R)
            {
                return 0;
            }
            else
            {
                double max = World.Instance.ControlledCar.Ultrasounds[0].maxReach;
                double val = i;
                if (val == max + 1)
                {
                    return 0;
                }

                return (max - val) / max * 100;
            }
        }

        public bool YellowThreshold(double i)
            => i > 50;

        public bool RedThreshold(double i)
            => i > 75;

        public object ColorSwitcher(double i, string red, string yellow, string green)
        {
            if (World.Instance.ControlledCar.VirtualFunctionBus.HMIPacket.Gear != Gears.R)
            {
                return Brush.Parse("#85929E");
            }
            else
            {
                double dist = this.DistanceConvert(i);
                if (this.RedThreshold(dist))
                {
                    return Brush.Parse(red);
                }
                else if (this.YellowThreshold(dist))
                {
                    return Brush.Parse(yellow);
                }
                else
                {
                    return Brush.Parse(green);
                }
            }
        }
    }
}
