namespace AutomatedCar.Models
{
    using System.Collections.Generic;
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public class CrossWalk : WorldObject
    {
        public CrossWalk(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix)
            : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, new List<List<Point>>())
        {
            this.IsColliding = false;
            this.ZIndex = 1;
        }
    }
}