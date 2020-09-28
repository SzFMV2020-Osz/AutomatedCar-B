namespace AutomatedCar.Models
{
    using Avalonia;

    public class Car : WorldObject
    {
        public Car(int x, int y, string filename)
            : base(x, y, filename)
        {
        }

        /// <summary>Gets or sets Speed in px/s.</summary>
        public int Speed { get; set; }

        protected void SetPositionPoint(Point point)
        {
            base.PositionPoint = point;
        }
    }
}