namespace AutomatedCar.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Avalonia;
    using NetTopologySuite.Geometries;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    public class Road : WorldObject
    {
        public Road(int x, int y, string filename, bool iscolliding, RotationMatrix rotmatrix, List<Polygon> roadplace)
            : base(x, y, filename, iscolliding, rotmatrix)
        {
            foreach (var place in roadplace)
            {
                var cords = new List<Coordinate>();
                cords.AddRange( place.Points.Select(p => new Coordinate(rotmatrix.Rotate(p).X - this.referenceOffsetX + this.X, rotmatrix.Rotate(p).Y-this.referenceOffsetY+this.Y)));
                this.NetPolygon.Add(new LineString(cords.ToArray()));
            }

            this.Polygons.AddRange(roadplace);
            this.ZIndex = 0;
        }
    }
}