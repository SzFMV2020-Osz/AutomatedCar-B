using System;
namespace AutomatedCar.Models {
    using Avalonia;
    using Avalonia.Controls.Shapes;

    /// <summary>This is a dummy object for demonstrating the codebase.</summary>
    public class Circle : IWorldObject {
        public Circle (int x, int y, string filename, int radius)
        {
            this.PositionPoint = new Point(x, y);
            this.FileName = filename;
            Radius = radius;
        }
        public int Radius { get; set; }

        public double CalculateArea () {
            return Math.PI * Radius * Radius;
        }

        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point PositionPoint { get; private set; } = new Point();
        public int ZIndex { get; set; }
        public Point RotationPoint { get; set; }
        public Polygon Polygon { get; set; }
        public bool IsCollidable { get; set; }
        public MatrixTwoByTwo RotationMatrix { get; set; }
        public bool IsHighlighted { get; set; }
    }
}