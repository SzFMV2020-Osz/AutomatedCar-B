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

            List<NetTopologySuite.Geometries.LineString> NTGeometry = (value as List<NetTopologySuite.Geometries.LineString>);
            List<Avalonia.Point> list = new List<Avalonia.Point>();

            foreach(var coordinate in NTGeometry[0].Coordinates)
            {
                list.Add(new Avalonia.Point(coordinate.X, coordinate.Y));
            }
            
            return list.ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}