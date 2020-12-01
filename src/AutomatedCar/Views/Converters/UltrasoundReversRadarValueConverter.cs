namespace AutomatedCar.Views.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia;

    class UltrasoundReversRadarValueConverter : UltrasoundConvert
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (World.Instance.ControlledCar.VirtualFunctionBus.HMIPacket.Gear != Gears.R)
            {
                return 0;
            }
            else
            {
                Ultrasound[] ultras = World.Instance.ControlledCar.Ultrasounds;

                double distance = ultras.Where(x => x.Points == (List<Point>)value).Select(x => x.Distance).FirstOrDefault();
                if (distance == ultras[0].maxReach + 1)
                {
                    return 0;
                }

                return distance / 50;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
