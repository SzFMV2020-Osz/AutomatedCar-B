using AutomatedCar.Models;
using AutomatedCar.SystemComponents;
using AutomatedCar.SystemComponents.Packets;
using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace Tests.SystemComponents
{
    public class UltrasoundTests
    {
        [Theory]
        [InlineData(10,30,80)]
        public void PointsCalculateCalculatesPoinsWhoseAreaIsTheRightSize(int offsetX, int offsetY, int rotate)
        {
            var w = World.Instance;
            w.ControlledCar = new AutomatedCar.Models.AutomatedCar(50, 50, "car_1_white", 108, 240, new List<List<Point>>());
            Ultrasound ultrasound = new Ultrasound(w.ControlledCar.VirtualFunctionBus, offsetX, offsetY, rotate);
            ultrasound.Points = ultrasound.CalculatePoints();
            double a = LineLenght(ultrasound.Points[0], ultrasound.Points[1]);
            double b = LineLenght(ultrasound.Points[0], ultrasound.Points[2]);
            double c = LineLenght(ultrasound.Points[2], ultrasound.Points[1]);
            double s = (a + b + c) / 2;
            double area = HeronFormula(s, a, b, c);
            double expectedArea = ultrasound.range * (2 * ultrasound.range * Math.Tan(ultrasound.angleOfView / 2 * Math.PI / 180)) / 2;
            Assert.Equal(Math.Round(area, 3), Math.Round(expectedArea, 3));
        }

        private double HeronFormula(double s, double a, double b, double c)
            => Math.Sqrt(s * (s - a) * (s - b) * (s - c));

        public double LineLenght(Point p1, Point p2)
            => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y,2));
    }
}
