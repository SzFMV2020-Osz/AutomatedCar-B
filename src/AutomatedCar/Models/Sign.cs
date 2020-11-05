namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using System.Collections.Generic;
    using System.Numerics;

    public class Sign : WorldObject, IMoveable
    {
        public Sign(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Point>> polyPoints) : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, polyPoints)
        {
            this.X = x;
            this.Y = y;
            this.IsColliding = true;
            this.ZIndex = 2;
        }

        private int _x;
        private int _y;

        public int Mass { get; set; } = 30;

        public void SetNextPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(Vector2 newPosition)
        {
            this.X = (int)newPosition.X;
            this.Y = (int)newPosition.Y;
        }
    }
}