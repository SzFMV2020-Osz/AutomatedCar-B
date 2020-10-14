using Avalonia;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Models
{
    public class TjunctionRoad : WorldObject
    {
        public TjunctionRoad(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Point>> polyPoints)
            : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, polyPoints)
        {
            this.ZIndex = 0;
            this.IsColliding = false;
        }
    }
}
