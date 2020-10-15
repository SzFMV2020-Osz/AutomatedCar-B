namespace AutomatedCar.Models
{
    using System.Numerics;
    using Avalonia;

    public class RotationMatrix
    {
        private Matrix innerMatrix;
        public RotationMatrix(double n11, double n12, double n21, double n22)
        {
            innerMatrix = new Matrix((float)n11, (float)n12, (float)n21, (float)n22, 1, 1);
            this.N11 = n11;
            this.N12 = n12;
            this.N21 = n21;
            this.N22 = n22;
        }

        public Point Rotate(Point point)
        {
            return point.Transform(innerMatrix);
        }

        public double N11 { get; set; }

        public double N12 { get; set; }

        public double N21 { get; set; }

        public double N22 { get; set; }
    }
}