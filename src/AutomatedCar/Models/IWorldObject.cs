namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public interface IWorldObject
    {
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point PositionPoint { get; }
        public int ZIndex { get; set; }
        public Point RotationPoint { get; set; }
        public Polygon Polygon { get; set; }
        public bool IsCollidable { get; set; }
        public MatrixTwoByTwo RotationMatrix { get; set; }
        public bool IsHighlighted { get; set; }
    }

    public class MatrixTwoByTwo
    {
        private double n11;
        private double n12;
        private double n21;
        private double n22;

        public MatrixTwoByTwo(double n11, double n12, double n21, double n22)
        {
            this.n11 = n11;
            this.n12 = n12;
            this.n21 = n21;
            this.n22 = n22;
        }

        public List<double> GetValues()
        {
            return new List<double> {n11, n12, n21, n22};
        }

    }
}