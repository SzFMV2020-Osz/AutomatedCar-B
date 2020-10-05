namespace AutomatedCar.Models
{
    using System;
    using System.Numerics;
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public class NPC : WorldObject, IMoveable
    {
        public NPCType Type { get; set; }

        public NPC(int x, int y, string filename, NPCType type)
            : base(x, y, filename)
        {
            this.Type = type;
        }

        public void SetNextPosition(int x, int y)
        {
            base.X = x;
            base.Y = y;
        }

        public void Move(Vector2 with)
        {
            throw new NotImplementedException();
        }
    }

    public enum NPCType
    {
        CAR, CYCLIST, PEDESTRIAN
    }
}