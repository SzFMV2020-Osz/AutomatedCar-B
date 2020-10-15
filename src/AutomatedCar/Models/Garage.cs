namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using System.Collections.Generic;

    public class Garage : WorldObject
    {
        public Garage(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Point>> polyPoints) : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, polyPoints)
        {
            this.IsColliding = false;
            this.ZIndex = 0;
        }
    }
}