namespace AutomatedCar.Models
{
    using System.Numerics;
    using Avalonia.Media;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using SystemComponents;
    using Avalonia;
    using ReactiveUI;
    using System;

    public class AutomatedCar : WorldObject, IMoveable
    {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;
        private HumanMachineInterface humanMachineInterface;
        private PowerTrain powerTrain;

        public ObservableCollection<DummySensor> Sensors { get; } = new ObservableCollection<DummySensor>();

        /*public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename, true,  new RotationMatrix(1.0, 0.0, 0.0, 1.0))*/
        public AutomatedCar(int x, int y, string filename, int width, int height, List<List<Point>> polylist)
            : base(x, y, filename, width, height, -width / 2, -height / 2, new Matrix(1, 0, 0, 1, 1, 1), polylist)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.humanMachineInterface = new HumanMachineInterface(this.virtualFunctionBus);
            this.powerTrain = new PowerTrain(this.virtualFunctionBus);
            this.dummySensor = new DummySensor(this.virtualFunctionBus);
            this.Brush = new SolidColorBrush(Color.Parse("red"));
            this.UltraSoundGeometries = createUltraSoundGeometries(generateUltraSoundPoints());

            this.ultraSoundVisible = true;
            this.radarVisible = true;
            this.cameraVisible = true;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public HumanMachineInterface HumanMachineInterface { get => this.humanMachineInterface; }

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

        public void MoveX(int x)
        {
            VisibleX = this.X - (World.Instance.VisibleWidth/2);
            this.X += x;
        }

        public void MoveY(int y)
        {
            VisibleY = this.Y - (World.Instance.VisibleHeight/2);
            this.Y += y;
        }

        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public SolidColorBrush RadarBrush { get; set; }

        public Geometry RadarGeometry { get; set; }

        private bool radarVisible;

        public bool RadarVisible
        {
            get => radarVisible;
            set => this.RaiseAndSetIfChanged(ref radarVisible, value); // virtualFunctionBus.DebugPacket.RadarSensor); A HMI olvasás hiányában most mockolt adattal jelenítjük meg.
        }

        public SolidColorBrush UltraSoundBrush { get; set; }

        public List<Geometry> UltraSoundGeometries { get; set; }

        private bool ultraSoundVisible;

        public bool UltraSoundVisible
        {
            get => ultraSoundVisible;
            set => this.RaiseAndSetIfChanged(ref ultraSoundVisible, value); //virtualFunctionBus.DebugPacket.UtrasoundSensor); A HMI olvasás hiányában most mockolt adattal jelenítjük meg.
        }

        public SolidColorBrush CameraBrush { get; set; }

        public Geometry CameraGeometry { get; set; }

        private bool cameraVisible;
        public bool CameraVisible
        {
            get => cameraVisible;
            set => this.RaiseAndSetIfChanged(ref cameraVisible, value); //virtualFunctionBus.DebugPacket.BoardCamera); A HMI olvasás hiányában most mockolt adattal jelenítjük meg.
        }
        public int VisibleX { get; set; }
        public int VisibleY { get; set; }
        
        private int healthPoints = 1;
        public int HealthPoints
        {
            get => healthPoints;
            set => this.RaiseAndSetIfChanged(ref healthPoints, value); 
        }
        
        /// <summary>
        /// Damages the car. The amount of damage is dependant on the momentary velocity vector of the car and the other object that was hit.
        /// </summary>
        /// <param name="carVector">The velocity vector of the car at the time of the impact</param>
        /// <param name="otherObjectVector">The velocity vector of the other object involved in the collision.</param>
        public void DamageOnCollision(Vector2 carVector, Vector2 otherObjectVector)
        {
            this.healthPoints -= Math.Abs(((int)carVector.X + (int)carVector.Y) - ((int)otherObjectVector.X + (int)otherObjectVector.Y));
        }

        /// <summary>Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }

        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        public void InputHandler()
        {
            humanMachineInterface.InputHandler();
        }

        private List<Geometry> createUltraSoundGeometries(List<Point> ultraSoundPoints)
        {
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
        private List<Point> generateUltraSoundPoints()
        {
            return new List<Point>()
            {
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