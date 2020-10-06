namespace AutomatedCar.Models
{
    using System.Numerics;
    using Avalonia.Media;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using SystemComponents;
    using Avalonia;

    public class AutomatedCar : WorldObject, IMoveable
    {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;
        private PowerTrain powerTrain;

        public ObservableCollection<DummySensor> Sensors { get; } = new ObservableCollection<DummySensor>();

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.IsCollidable = true;
            this.IsHighlighted = false;
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.dummySensor = new DummySensor(this.virtualFunctionBus);
            this.powerTrain = new PowerTrain(this.virtualFunctionBus);
            this.Brush = new SolidColorBrush(Color.Parse("red"));
            this.UltraSoundGeometries = createUltraSoundGeometries(generateUltraSoundPoints());
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public Geometry Geometry { get; set; }

        public SolidColorBrush Brush { get; private set; }

        public int Speed { get; set; }

        /// <summary>
        /// Example usage add
        /// </summary>
        /// <param name="point"></param>
        public void SetNextPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Move(Vector2 newPosition)
        {
            this.X = (int)newPosition.X;
            this.Y = (int)newPosition.Y;
        }

        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public SolidColorBrush RadarBrush { get; set; }

        public Geometry RadarGeometry { get; set; }

        public bool RadarVisible { get; set; }

        public SolidColorBrush UltraSoundBrush { get; set; }

        public List<Geometry> UltraSoundGeometries { get; set; }

        public bool UltraSoundVisible { get; set; } 

        /// <summary>Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }

        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        private List<Geometry> createUltraSoundGeometries(List<Point> ultraSoundPoints){
            
            // List<Geometry> ultraSoundGeometries = new List<Geometry>().;
            // for (int i = 0; i < 8; i++)
            // {
            //     ultraSoundGeometries.Add(new PolylineGeometry(ultraSoundPoints.GetRange(i * 3, 3), false));
            // }
            
            return new List<Geometry>
            {
                new PolylineGeometry(ultraSoundPoints.GetRange(0 * 3, 3), false),
                new PolylineGeometry(ultraSoundPoints.GetRange(1 * 3, 3), false),
                new PolylineGeometry(ultraSoundPoints.GetRange(2 * 3, 3), false),
                new PolylineGeometry(ultraSoundPoints.GetRange(3 * 3, 3), false),
                new PolylineGeometry(ultraSoundPoints.GetRange(4 * 3, 3), false),
                new PolylineGeometry(ultraSoundPoints.GetRange(5 * 3, 3), false),
                new PolylineGeometry(ultraSoundPoints.GetRange(6 * 3, 3), false),
                new PolylineGeometry(ultraSoundPoints.GetRange(7 * 3, 3), false)
            };
        }
        private List<Point> generateUltraSoundPoints(){
            return new List<Point>(){
            new Point(51, 239),
            new Point(10, 10),
            new Point(200, 300),
            new Point(18, 231),
            new Point(200, 100),
            new Point(100, 300),
            new Point(0, 92),
            new Point(200, 100),
            new Point(100, 300),
            new Point(7, 27),
            new Point(200, 100),
            new Point(100, 300),
            new Point(17, 10),
            new Point(200, 100),
            new Point(100, 300),
            new Point(40, 2),
            new Point(200, 100),
            new Point(100, 300),
            new Point(79, 5),
            new Point(200, 100),
            new Point(100, 300),
            new Point(99, 91),
            new Point(200, 100),
            new Point(100, 300)
            };
        }
    }
}