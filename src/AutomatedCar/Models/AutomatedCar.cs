namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using SystemComponents;
    using Avalonia;

    public class AutomatedCar : Car, IMoveable
    {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.dummySensor = new DummySensor(this.virtualFunctionBus);
            this.Brush = new SolidColorBrush(Color.Parse("red"));
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public Geometry Geometry { get; set; }

        public SolidColorBrush Brush { get; private set; }

        public Point Speed { get; }

        public Point Acceleration { get; }

        public Point PositionPoint { get; private set; }

        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        /// <summary>Stops the automated cor by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }

        public void SetNextPosition(Point point)
        {
            base.SetPositionPoint(new Point(point.X, point.Y));
        }
    }
}