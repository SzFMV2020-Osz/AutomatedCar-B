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
        public double distance = 0;
        public Point[] computeTriangleInWorld()
        {
            Point[] points = new Point[3];


            double atfogo = distance/Math.Cos((Math.PI / 180) * 30);
            double a2 = Math.Pow(atfogo, 2)-Math.Pow(distance, 2);

            double eredmeny = Math.Round(Math.Sqrt(a2));


            points[0] = new Point(0,this.offset);
            points[1] = new Point(eredmeny,this.offset + this.distance);
            points[2] = new Point(-eredmeny,this.offset + this.distance);

            return points;
        }
    }
}