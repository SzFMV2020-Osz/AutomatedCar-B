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
        private Ultrasound[] ultrasounds;

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
            this.Ultrasounds = new Ultrasound[]
            {
                new Ultrasound(this.virtualFunctionBus, 110, 30, 0),
                new Ultrasound(this.virtualFunctionBus, 105, 45, 90),
                new Ultrasound(this.virtualFunctionBus, 110, -30, 0),
                new Ultrasound(this.virtualFunctionBus, 105, -45, -90),
                new Ultrasound(this.virtualFunctionBus, -120, 25, 180),
                new Ultrasound(this.virtualFunctionBus, -105, 45, 90),
                new Ultrasound(this.virtualFunctionBus, -120, -25, 180),
                new Ultrasound(this.virtualFunctionBus, -105, -45, -90),
            };

            this.ultraSoundVisible = true;
            this.radarVisible = true;
            this.cameraVisible = true;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public HumanMachineInterface HumanMachineInterface { get => this.humanMachineInterface; }

        public Ultrasound[] Ultrasounds { get => this.ultrasounds; set { this.RaiseAndSetIfChanged(ref this.ultrasounds, value); } }

        public Ultrasound Ultrasound0 { get => this.ultrasounds[0]; set { this.RaiseAndSetIfChanged(ref this.ultrasounds[0], value); } }

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
            set => this.RaiseAndSetIfChanged(ref radarVisible, value);
        }

        private bool ultraSoundVisible;

        public bool UltraSoundVisible
        {
            get => ultraSoundVisible;
            set => this.RaiseAndSetIfChanged(ref ultraSoundVisible, value);
        }

        public SolidColorBrush CameraBrush { get; set; }

        public Geometry CameraGeometry { get; set; }

        private bool cameraVisible;

        public bool CameraVisible
        {
            get => cameraVisible;
            set => this.RaiseAndSetIfChanged(ref cameraVisible, value);
        }

        public int VisibleX { get; set; }

        public int VisibleY { get; set; }
        
        private int healthPoints = 100;
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
    }
}