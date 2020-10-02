namespace AutomatedCar.Models
{
    using System;
    using Avalonia;

    public interface IMoveable
    {
        public void SetNextPosition(int x, int y);

        public Point Speed { get; set; }

        public Point Acceleration { get; set; }
    }
}