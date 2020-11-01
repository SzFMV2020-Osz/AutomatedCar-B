using Avalonia;
using NetTopologySuite.GeometriesGraph;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Models.RadarUtil
{
    public class RadarTriangleComputer
    { 
        public int offset = 0;
        public int distance = 0;
        public int rotate = 0;
        public int angle = 30;
        public int carX = 0;
        public int carY = 0;
        public Point[] computeTriangleInWorld()
        {
            Point[] points = new Point[3];

            var ca = cos(rotate);
            var sa = sin(rotate);

            int triangleBottomHalf = getTriangleBottomHalf((int)(this.distance), this.angle);

            points[0] = new Point(
                this.offset * ca + carX,
                this.offset * sa + carY
                );
            points[1] = new Point(
                this.offset * ca + distance * ca - triangleBottomHalf * sa + carX,
                this.offset * sa + distance * sa + triangleBottomHalf * ca + carY
                );
            points[2] = new Point(
                this.offset * ca + distance * ca + triangleBottomHalf * sa + carX,
                this.offset * sa + distance * sa - triangleBottomHalf * ca + carY
                );

            return points;
        }

        private int getTriangleBottomHalf(int distance, int angle) {
            double hypotenuse = distance / Math.Cos((Math.PI / 180) * angle);
            double powered = Math.Pow(hypotenuse, 2) - Math.Pow(distance, 2);

            return (int)Math.Round(Math.Sqrt(powered));
        }

        private double cos(int rotate){
            return Math.Round(Math.Cos((Math.PI / 180) * rotate));
        }

        private double sin(int rotate){
            return Math.Round(Math.Sin((Math.PI / 180) * rotate));
        }
    }
}