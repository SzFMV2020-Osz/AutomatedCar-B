namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public class NPC: IWorldObject, IMoveable
    {
        public NPCType Type { get; set; }
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point PositionPoint { get; private set; }
        public int ZIndex { get; set; }
        public Point RotationPoint { get; set; }
        public Polygon Polygon { get; set; }
        public bool IsCollidable { get; set; }
        public MatrixTwoByTwo RotationMatrix { get; set; }
        public bool IsHighlighted { get; set; }
        public Point Speed { get; }
        public Point Acceleration { get; }
        

        public NPC(NPCType type, string fileName, int width, int height, Point positionPoint, int zIndex, Point rotationPoint, Polygon polygon, bool isCollidable, MatrixTwoByTwo rotationMatrix, bool isHighlighted)
        {
            Type = type;
            FileName = fileName;
            Width = width;
            Height = height;
            PositionPoint = positionPoint;
            ZIndex = zIndex;
            RotationPoint = rotationPoint;
            Polygon = polygon;
            IsCollidable = isCollidable;
            RotationMatrix = rotationMatrix;
            IsHighlighted = isHighlighted;
        }

        public void SetNextPosition(Point point)
        {
            this.PositionPoint = new Point(point.X, point.Y);
        }


    }

    public enum NPCType
    {
        CAR, CYCLIST, PEDESTRIAN, BIRD, INSECT, HELICOPTER
    }
}