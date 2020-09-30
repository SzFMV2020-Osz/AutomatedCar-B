namespace AutomatedCar.Models
{
    using Avalonia;

    public interface IMoveable
    {
        public void SetNextPosition(Point point);

        public Point Speed { get; set; }

        public Point Acceleration { get; set; }
    }
}