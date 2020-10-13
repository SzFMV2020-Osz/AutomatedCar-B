namespace AutomatedCar.Models
{
    using Avalonia.Controls.Shapes;
    using System.Collections.Generic;

    public class Sign : WorldObject
    {
        public Sign(int x, int y, string filename, bool iscolliding, RotationMatrix rotmatrix, Polygon bollard)
            : base(x, y, filename, iscolliding, rotmatrix)
        {
            this.Polygons.Add(bollard);
            this.ZIndex = 2;
        }
    }
}