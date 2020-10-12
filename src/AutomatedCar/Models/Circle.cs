namespace AutomatedCar.Models
{
    using Avalonia.Controls.Shapes;
    using System;
    using System.Collections.Generic;

    /// <summary>This is a dummy object for demonstrating the codebase.</summary>
    public class Circle : WorldObject
    {
        public Circle(int x, int y, string filename, int radius, bool iscolliding, RotationMatrix rotationMatrix, Polygon polygon)
            : base(x, y, filename, iscolliding, rotationMatrix)
        {
            this.Radius = radius;
            this.Polygons.Add(polygon);
        }

        public int Radius { get; set; }

        public double CalculateArea()
        {
            return Math.PI * this.Radius * this.Radius;
        }
    }
}