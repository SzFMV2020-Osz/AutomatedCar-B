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
        public Point[] computeTriangleInWorld()
        {
            Point[] points = new Point[3];

            var ca = cos(rotate);
            var sa = sin(rotate);

            int y = getXfromAngleAndDistance((int)(this.distance*ca), this.angle);

            points[0] = new Point(this.offset * ca, this.offset * sa);
            points[1] = new Point(this.offset * ca + distance * ca, this.offset * sa + y);
            points[2] = new Point(this.offset * ca + distance * ca, this.offset * sa - y);

            return points;
        }

        private int getXfromAngleAndDistance(int distance, int angle) {
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