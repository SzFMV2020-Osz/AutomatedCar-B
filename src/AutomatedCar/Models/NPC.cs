namespace AutomatedCar.Models
{
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

        public void SetNextPosition(Point point)
        {
            //base.PositionPoint = new Point(point.X, point.Y);
        }
    }

    public enum NPCType
    {
        CAR, CYCLIST, PEDESTRIAN
    }
}