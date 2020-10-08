namespace AutomatedCar.Models
{
    public class RotationMatrix
    {
        public RotationMatrix(double n11, double n12, double n21, double n22)
        {
            this.N11 = n11;
            this.N12 = n12;
            this.N21 = n21;
            this.N22 = n22;
        }

        public double N11 { get; set; }

        public double N12 { get; set; }

        public double N21 { get; set; }

        public double N22 { get; set; }
    }
}