using AutomatedCar.SystemComponents;
using Avalonia.Media;

namespace AutomatedCar.Models {
    using SystemComponents;
    using Avalonia;
    using Avalonia.Controls.Shapes;

    public class AutomatedCar : Car, IWorldObject, IMoveable {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;

        public string FileName { get; set; }
        public Point PositionPoint { get; private set; }
        public Point RotationPoint { get; set; }
        public Polygon Polygon { get; set; }
        public bool IsCollidable { get; set; }
        public MatrixTwoByTwo RotationMatrix { get; set; }
        public bool IsHighlighted { get; set; }
        public Point Speed { get; }
        public Point Acceleration { get; }

        public AutomatedCar (int x, int y, string filename) : base (x, y, filename) {
            virtualFunctionBus = new VirtualFunctionBus ();
            dummySensor = new DummySensor (virtualFunctionBus);
            Brush = new SolidColorBrush (Color.Parse ("red"));
        }

        public AutomatedCar(int x, int y, string filename, string fileName, Point positionPoint, Point rotationPoint, Polygon polygon, bool isCollidable, MatrixTwoByTwo rotationMatrix, bool isHighlighted) : base(x, y, filename)
        {
            virtualFunctionBus = new VirtualFunctionBus ();
            dummySensor = new DummySensor (virtualFunctionBus);
            Brush = new SolidColorBrush (Color.Parse ("red"));
            FileName = fileName;
            PositionPoint = positionPoint;
            RotationPoint = rotationPoint;
            Polygon = polygon;
            IsCollidable = isCollidable;
            RotationMatrix = rotationMatrix;
            IsHighlighted = isHighlighted;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => virtualFunctionBus; }
        public Geometry Geometry { get; set; }
        public SolidColorBrush Brush { get; private set; }

        public void Start () {
            virtualFunctionBus.Start ();
        }
        public void Stop () {
            virtualFunctionBus.Stop ();
        }

        public void SetNextPosition(Point point)
        {
            this.PositionPoint = new Point(point.X, point.Y);
        }
        
    }
}