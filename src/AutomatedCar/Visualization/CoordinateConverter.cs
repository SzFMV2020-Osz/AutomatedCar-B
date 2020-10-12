namespace AutomatedCar.Visualization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Avalonia.Data.Converters;
    using NetTopologySuite.Geometries;

    public class CoordinateConverter : IValueConverter
    {
        public static CoordinateConverter Instance { get; } = new CoordinateConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var g = (value as NetTopologySuite.Geometries.LineString);
            var list = new List<Avalonia.Point>();

            foreach(var n in g.Coordinates){
                list.Add(new Avalonia.Point(n.X,n.Y));
            }
            
            return list.ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}