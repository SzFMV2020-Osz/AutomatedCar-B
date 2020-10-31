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
        public Point[] computeTriangleInWorld()
        {
            Point[] points = new Point[3];


            int xWidth = getXfromAngleAndDistance(this.distance, 30);


            points[0] = new Point(0,this.offset);
            points[1] = new Point(xWidth,this.offset + this.distance);
            points[2] = new Point(-xWidth,this.offset + this.distance);

            return points;
        }

        private int getXfromAngleAndDistance(int distance, int angle) {
            double hypotenuse = distance/Math.Cos((Math.PI / 180) * angle);
            double powered = Math.Pow(hypotenuse, 2)-Math.Pow(distance, 2);

            return (int)Math.Round(Math.Sqrt(powered));
        }
    }
}