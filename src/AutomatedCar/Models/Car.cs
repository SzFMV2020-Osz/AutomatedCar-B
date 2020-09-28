namespace AutomatedCar.Models
{
    using Avalonia;

    public class Car : WorldObject, IMoveable
    {
        public Car(int x, int y, string filename)
            : base(x, y, filename)
        {
        }

        protected void SetPositionPoint(Point point)
        {
            base.PositionPoint = point;
        }

        public void SetNextPosition(Point point)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>Gets or sets Speed in px/s.</summary>
        public Point Speed { get; set; }

        /// <summary>
        /// Alternative for Speed. Not decided yet.
        /// </summary>
        public Point Acceleration { get; set; }

    }
}