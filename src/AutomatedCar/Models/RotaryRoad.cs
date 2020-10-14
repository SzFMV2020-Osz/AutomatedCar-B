namespace AutomatedCar.Models
{
    using System.Collections.Generic;
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public class RotaryRoad : WorldObject
    {
        public RotaryRoad(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Point>> polyPoints)
            : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, polyPoints)
        {
            this.IsColliding = false;
            this.ZIndex = 0;
        }
    }
}