using Avalonia;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Models
{
    public class Pedestrian : WorldObject, INPC
    {
        public Pedestrian(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Point>> polyPoints) : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, polyPoints)
        {
        }

        public int Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
