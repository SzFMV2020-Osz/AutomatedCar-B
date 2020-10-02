namespace AutomatedCar.Models
{
    using System;
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public class NPC : WorldObject, IMoveable
    {
        public NPCType Type { get; set; }

        public Point Speed { get; set; }

        public Point Acceleration { get; set; }

        public NPC(int x, int y, string filename, NPCType type, Point speed, Point acceleration)
            : base(x, y, filename)
        {
            this.Type = type;
            this.Speed = speed;
            this.Acceleration = acceleration;
        }

        public void SetNextPosition(int x, int y)
        {
            base.X = x;
            base.Y = y;
        }
    }

    public enum NPCType
    {
        CAR, CYCLIST, PEDESTRIAN
    }
}