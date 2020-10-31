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
        public Point[] computeTriangleInWorld()
        {
            Point[] points = new Point[3];

            points[0] = new Point(0,this.offset);
            points[1] = new Point(0,this.offset);
            points[2] = new Point(0,this.offset);

            return points;
        }
    }
}