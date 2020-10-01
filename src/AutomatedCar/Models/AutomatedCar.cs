namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using SystemComponents;
    using Avalonia;

    public class AutomatedCar : WorldObject, IMoveable
    {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.IsCollidable = true;
            this.IsHighlighted = false;
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.dummySensor = new DummySensor(this.virtualFunctionBus);
            this.Brush = new SolidColorBrush(Color.Parse("red"));
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public Geometry Geometry { get; set; }

        public SolidColorBrush Brush { get; private set; }

        public void SetNextPosition(Point point)
        {
            //this.PositionPoint = point;
        }

        /// <summary>Gets or sets Speed in px/s.</summary>
        public Point Speed { get; set; }

        /// <summary>
        /// Alternative for Speed. Not decided yet.
        /// </summary>
        public Point Acceleration { get; set; }

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
    }
}