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


            int y = getXfromAngleAndDistance(this.distance, this.angle);


            var ca = Math.Round(Math.Cos((Math.PI / 180) * rotate)); //0 1 // 90 0 //180 -1 // 270 -0
            var sa = Math.Round(Math.Sin((Math.PI / 180) * rotate)); // 0 0 //90 1 //180 -0 // 270 -1


            points[0] = new Point(this.offset*ca, this.offset*sa);
            points[1] = new Point((this.offset + this.distance),y);
            points[2] = new Point((this.offset + this.distance), (-y));

            return points;
        }

        private int getXfromAngleAndDistance(int distance, int angle) {
            double hypotenuse = distance/Math.Cos((Math.PI / 180) * angle);
            double powered = Math.Pow(hypotenuse, 2)-Math.Pow(distance, 2);

            return (int)Math.Round(Math.Sqrt(powered));
        }
    }
}