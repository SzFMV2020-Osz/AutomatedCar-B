namespace AutomatedCar.Models
{
    using System.Collections.Generic;
    using Avalonia.Controls.Shapes;

    public class Road : WorldObject
    {
        public Road(int x, int y, string filename, bool iscolliding, RotationMatrix rotmatrix, List<Polygon> roadplace)
            : base(x, y, filename, iscolliding, rotmatrix)
        {
            this.Polygons.AddRange(roadplace);
            this.ZIndex = 0;
        }
    }
}