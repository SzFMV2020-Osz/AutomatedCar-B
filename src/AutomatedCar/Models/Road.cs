namespace AutomatedCar.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia;
    using NetTopologySuite.Geometries;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    public class Road : WorldObject
    {
        public Road(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Avalonia.Point>> polyPoints)
            : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, polyPoints)
        {
            this.IsColliding = false;
            this.ZIndex = 0;
        }
    }
}