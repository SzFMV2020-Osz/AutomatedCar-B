namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public class NPC
    {
        public NPCType Type { get; set; }

        public string FileName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Point PositionPoint { get; private set; }

        public int ZIndex { get; set; }

        public Point RotationPoint { get; set; }

        public Polygon Polygon { get; set; }

        public bool IsCollidable { get; private set; }

        public MatrixTwoByTwo RotationMatrix { get; set; }

        public bool IsHighlighted { get; set; }

        public Point Speed { get; }

        public Point Acceleration { get; }

        public NPC(NPCType type, string fileName, int width, int height, Point positionPoint, int zIndex, Point rotationPoint, Polygon polygon, bool isCollidable, MatrixTwoByTwo rotationMatrix, bool isHighlighted)
        {
            this.Type = type;
            this.FileName = fileName;
            this.Width = width;
            this.Height = height;
            this.PositionPoint = positionPoint;
            this.ZIndex = zIndex;
            this.RotationPoint = rotationPoint;
            this.Polygon = polygon;
            this.IsCollidable = isCollidable;
            this.RotationMatrix = rotationMatrix;
            this.IsHighlighted = isHighlighted;
        }

        public void SetNextPosition(Point point)
        {
            this.PositionPoint = new Point(point.X, point.Y);
        }
    }

    public enum NPCType
    {
        CAR, CYCLIST, PEDESTRIAN
    }
}